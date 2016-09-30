using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EximLogAnalyzer.Models;

namespace EximLogAnalyzer
{
    public class LogParser
    {
        private int _currentLogLineNumber;
        public List<string> BounceMessages = new List<string>();
        public List<string> DeliveryDeferred = new List<string>();
        public List<string> DovecotAuthenticatorFailed = new List<string>();
        public List<string> IdentConnection = new List<string>();
        public List<string> MailBoxIsFull = new List<string>();
        public List<string> MessageIsFrozen = new List<string>();
        public List<string> NotFullTransaction = new List<string>();
        public List<string> NotParsedLogLines = new List<string>();
        public List<string> RetryTimeNotReachedForAnyHost = new List<string>();
        public List<string> SorryWeAreNotOpenRelay = new List<string>();
        public List<string> TlsErrorOnConnection = new List<string>();
        private Dictionary<int, int> _parsedLogLines = new Dictionary<int, int>();
        public Dictionary<string, Mail> ResultDict = new Dictionary<string, Mail>();

        private void ClearResults()
        {
            BounceMessages.Clear();
            DeliveryDeferred.Clear();
            DovecotAuthenticatorFailed.Clear();
            IdentConnection.Clear();
            MailBoxIsFull.Clear();
            MessageIsFrozen.Clear();
            NotFullTransaction.Clear();
            NotParsedLogLines.Clear();
            RetryTimeNotReachedForAnyHost.Clear();
            SorryWeAreNotOpenRelay.Clear();
            TlsErrorOnConnection.Clear();
            _parsedLogLines.Clear();
            ResultDict.Clear();
        }

        public void ParseEximMainLog(Stream inputStream)
        {
            ClearResults();
            inputStream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(inputStream, true);
            while (!streamReader.EndOfStream)
            {
                _currentLogLineNumber++;
                var logLine = streamReader.ReadLine();
                ParseLogLine(logLine);
                if (!_parsedLogLines.ContainsKey(_currentLogLineNumber))
                {
                    NotParsedLogLines.Add(logLine);
                }
            }
        }

        private void ParseLogLine(string logLine)
        {
            if (logLine.Contains("cwd=/var/spool/exim4") || logLine.Contains("DKIM:") ||
                logLine.Contains("Start queue run:") || logLine.Contains("End queue run:") ||
                logLine.Contains("no host name found for IP address"))
            {
                MarkCurrentLogLineAsParsed();
                return;
            }

            if (logLine.Contains("SMTP connection from"))
            {
                MarkCurrentLogLineAsParsed();
                return;
            }

            if (logLine.Contains("dovecot_login authenticator failed"))
            {
                MarkCurrentLogLineAsParsed();
                DovecotAuthenticatorFailed.Add(logLine);
                return;
            }

            if (logLine.Contains("dovecot_gssapi authenticator failed "))
            {
                MarkCurrentLogLineAsParsed();
                DovecotAuthenticatorFailed.Add(logLine);
                return;
            }

            if (logLine.Contains("TLS error on connection"))
            {
                MarkCurrentLogLineAsParsed();
                TlsErrorOnConnection.Add(logLine);
                return;
            }

            if (logLine.Contains("ident connection"))
            {
                MarkCurrentLogLineAsParsed();
                IdentConnection.Add(logLine);
                return;
            }

            if (logLine.Contains("Message is frozen"))
            {
                MarkCurrentLogLineAsParsed();
                MessageIsFrozen.Add(logLine);
                return;
            }

            if (logLine.Contains("retry time not reached for any host") ||
                logLine.Contains("Retry time not yet reached"))
            {
                MarkCurrentLogLineAsParsed();
                RetryTimeNotReachedForAnyHost.Add(logLine);
                return;
            }

            if (logLine.Contains("mailbox is full"))
            {
                MarkCurrentLogLineAsParsed();
                MailBoxIsFull.Add(logLine);
            }

            if (logLine.Contains("Sorry, we are not open relay"))
            {
                MarkCurrentLogLineAsParsed();
                SorryWeAreNotOpenRelay.Add(logLine);
                return;
            }

            var regex =
                new Regex(
                    @"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) (<=|=>|->|Completed|\*\*|==) (<>)?");
            var match = regex.Match(logLine);
            if (match.Success)
            {
                Mail mail;
                var mailId = match.Groups[3].Value;
                var direction = match.Groups[4].Value;
                if (ResultDict.ContainsKey(mailId))
                {
                    mail = ResultDict[mailId];
                    if (match.Groups[5].Value == "<>")
                    {
                        ParseBounceMessage(logLine, mail);
                        return;
                    }

                    if (direction == "=>" || direction == "->")
                    {
                        ParseOutputMessage(logLine, mail);
                        return;
                    }

                    if (direction == "**")
                    {
                        ParseDeliveryFailedAddressBouncedLogLine(logLine, mail);
                        return;
                    }

                    if (direction == "==")
                    {
                        ParseDeliveryDeferredTemporaryProblemLogLine(logLine, mail);
                        return;
                    }

                    if (direction == "Completed")
                    {
                        MarkCurrentLogLineAsParsed();
                        mail.EndTime = DateTime.Parse(match.Groups[1].Value);
                        mail.Completed = true;
                    }
                }
                else
                {
                    if (direction == "<=")
                    {
                        if (match.Groups[5].Value == "<>")
                        {
                            mail = new Mail();
                            ResultDict.Add(mailId, mail);
                            mail.Id = match.Groups[3].Value;
                            mail.StartTime = DateTime.Parse(match.Groups[1].Value);
                            ParseBounceMessage(logLine, mail);
                        }
                        else
                        {
                            mail = new Mail();
                            ResultDict.Add(mailId, mail);
                            mail.Id = match.Groups[3].Value;
                            mail.StartTime = DateTime.Parse(match.Groups[1].Value);
                            ParseReceivedMessage(logLine, mail);
                        }
                    }
                    else
                    {
                        MarkCurrentLogLineAsParsed();
                        NotFullTransaction.Add(logLine);
                    }
                }
            }
        }

        private void ParseDeliveryDeferredTemporaryProblemLogLine(string logLine, Mail mail)
        {
            var handled = false;
            var regex1 =
                new Regex(
                    @"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) == (.+?) \((.+)\) <(.+?)> R=.+defer \(-?\d{0,3}\):(.+)");
            var match1 = regex1.Match(logLine);
            if (match1.Success)
            {
                var deliveryStatus =
                    mail.DeliveryStatus.Where(t => t.RecipientAddress == match1.Groups[5].Value.Trim().ToLower())
                        .FirstOrDefault();
                if (deliveryStatus == null)
                {
                    deliveryStatus = new DeliveryStatus();
                    deliveryStatus.RecipientAddress = match1.Groups[5].Value.Trim().ToLower();
                    deliveryStatus.StatusMessage = string.Format("{0}: {1}", match1.Groups[1].Value,
                        match1.Groups[7].Value.Trim());
                    deliveryStatus.Deferred = true;
                    deliveryStatus.MailId = mail.Id;
                    mail.DeliveryStatus.Add(deliveryStatus);
                }
                else
                {
                    deliveryStatus.StatusMessage += string.Format("\r\n{0}: {1}", match1.Groups[1].Value,
                        match1.Groups[7].Value.Trim());
                }
                handled = true;
            }
            if (handled == false)
            {
                var regex2 =
                    new Regex(
                        @"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) == (.+?) <(.+?)> R=.+defer \(-?\d{0,3}\):(.+)");
                var match2 = regex2.Match(logLine);
                if (match2.Success)
                {
                    var deliveryStatus =
                        mail.DeliveryStatus.Where(t => t.RecipientAddress == match2.Groups[5].Value.Trim().ToLower())
                            .FirstOrDefault();
                    if (deliveryStatus == null)
                    {
                        deliveryStatus = new DeliveryStatus();
                        deliveryStatus.RecipientAddress = match2.Groups[5].Value.Trim().ToLower();
                        deliveryStatus.StatusMessage = string.Format("{0}: {1}", match2.Groups[1].Value,
                            match2.Groups[6].Value.Trim());
                        deliveryStatus.Deferred = true;
                        deliveryStatus.MailId = mail.Id;
                        mail.DeliveryStatus.Add(deliveryStatus);
                    }
                    else
                    {
                        deliveryStatus.StatusMessage += string.Format("\r\n{0}: {1}", match2.Groups[1].Value,
                            match2.Groups[6].Value.Trim());
                    }
                    handled = true;
                }
            }
            if (handled == false)
            {
                var regex3 =
                    new Regex(
                        @"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) == (.+?\s).+T=.+defer \(-?\d{0,3}\):(.+)");
                var match3 = regex3.Match(logLine);
                if (match3.Success)
                {
                    var deliveryStatus =
                        mail.DeliveryStatus.Where(t => t.RecipientAddress == match3.Groups[4].Value.Trim().ToLower())
                            .FirstOrDefault();
                    if (deliveryStatus == null)
                    {
                        deliveryStatus = new DeliveryStatus();
                        deliveryStatus.RecipientAddress = match3.Groups[4].Value.Trim().ToLower();
                        deliveryStatus.StatusMessage = string.Format("{0}: {1}", match3.Groups[1].Value,
                            match3.Groups[5].Value.Trim());
                        deliveryStatus.Deferred = true;
                        deliveryStatus.MailId = mail.Id;
                        mail.DeliveryStatus.Add(deliveryStatus);
                    }
                    else
                    {
                        deliveryStatus.StatusMessage = deliveryStatus.StatusMessage +
                                                       string.Format("\r\n{0}: {1}", match3.Groups[1].Value,
                                                           match3.Groups[5].Value.Trim());
                    }
                    handled = true;
                }
            }

            if (handled == false)
            {
                var regex =
                    new Regex(
                        @"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) == (.+?\s).+defer \(-?\d{0,3}\):(.+)");
                var match = regex.Match(logLine);
                if (match.Success)
                {
                    var deliveryStatus =
                        mail.DeliveryStatus.Where(t => t.RecipientAddress == match.Groups[4].Value.Trim().ToLower())
                            .FirstOrDefault();
                    if (deliveryStatus == null)
                    {
                        deliveryStatus = new DeliveryStatus();
                        deliveryStatus.RecipientAddress = match.Groups[4].Value.Trim().ToLower();
                        deliveryStatus.StatusMessage = string.Format("{0}: {1}", match.Groups[1].Value,
                            match.Groups[5].Value);
                        deliveryStatus.Deferred = true;
                        deliveryStatus.MailId = mail.Id;
                        mail.DeliveryStatus.Add(deliveryStatus);
                    }
                    else
                    {
                        deliveryStatus.StatusMessage = deliveryStatus.StatusMessage +
                                                       string.Format("\r\n{0}: {1}", match.Groups[1].Value,
                                                           match.Groups[5].Value.Trim());
                    }
                    handled = true;
                }
            }
            if (handled)
            {
                MarkCurrentLogLineAsParsed();
            }
            DeliveryDeferred.Add(logLine);
        }

        private void ParseDeliveryFailedAddressBouncedLogLine(string logLine, Mail mail)
        {
            var handled = false;
            var handlerCount = 0;

            var regex1 =
                new Regex(@"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) \*\* (.+?)\s.+T=.+?:(.+)");
            var match1 = regex1.Match(logLine);
            if (match1.Success)
            {
                var deliveryStatus =
                    mail.DeliveryStatus.Where(t => t.RecipientAddress == match1.Groups[4].Value.Trim().ToLower())
                        .FirstOrDefault();
                if (deliveryStatus == null)
                {
                    deliveryStatus = new DeliveryStatus();
                    deliveryStatus.RecipientAddress = match1.Groups[4].Value.ToLower();
                    deliveryStatus.StatusMessage = string.Format("{0}: {1}", match1.Groups[1].Value,
                        match1.Groups[5].Value.Trim());
                    deliveryStatus.Delivered = false;
                    deliveryStatus.Deferred = false;
                    deliveryStatus.MailId = mail.Id;
                    mail.DeliveryStatus.Add(deliveryStatus);
                }
                else
                {
                    deliveryStatus.StatusMessage += string.Format("\r\n{0}: {1}", match1.Groups[1].Value,
                        match1.Groups[5].Value.Trim());
                }
                handled = true;
                handlerCount++;
                MarkCurrentLogLineAsParsed();
            }

            if (handled == false)
            {
                var regex2 =
                    new Regex(@"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) \*\* (.+?)\s.+?:(.+)");
                var match2 = regex2.Match(logLine);
                if (match2.Success)
                {
                    var deliveryStatus =
                        mail.DeliveryStatus.Where(t => t.RecipientAddress == match2.Groups[4].Value.Trim().ToLower())
                            .FirstOrDefault();
                    if (deliveryStatus == null)
                    {
                        deliveryStatus = new DeliveryStatus();
                        deliveryStatus.Deferred = false;
                        deliveryStatus.Delivered = false;
                        deliveryStatus.MailId = mail.Id;
                        deliveryStatus.RecipientAddress = match2.Groups[4].Value.ToLower();
                        deliveryStatus.StatusMessage = string.Format("{0}: {1}", match2.Groups[1].Value,
                            match2.Groups[5].Value.Trim());
                        mail.DeliveryStatus.Add(deliveryStatus);
                    }
                    else
                    {
                        deliveryStatus.StatusMessage += string.Format("\r\n{0}: {1}", match2.Groups[1].Value,
                            match2.Groups[5].Value.Trim());
                    }
                    handled = true;
                    handlerCount++;
                    MarkCurrentLogLineAsParsed();
                }
            }
            if (handled == false)
            {
                var regex3 =
                    new Regex(@"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) \*\* (.+?):(.+)");
                var match3 = regex3.Match(logLine);
                if (match3.Success)
                {
                    var deliveryStatus =
                        mail.DeliveryStatus.Where(t => t.RecipientAddress == match3.Groups[4].Value.Trim().ToLower())
                            .FirstOrDefault();
                    if (deliveryStatus == null)
                    {
                        deliveryStatus = new DeliveryStatus();
                        deliveryStatus.RecipientAddress = match3.Groups[4].Value.ToLower();
                        deliveryStatus.StatusMessage = string.Format("{0}: {1}", match3.Groups[1].Value,
                            match3.Groups[5].Value.Trim());
                        deliveryStatus.Delivered = false;
                        deliveryStatus.Deferred = false;
                        deliveryStatus.MailId = mail.Id;
                        mail.DeliveryStatus.Add(deliveryStatus);
                    }
                    else
                    {
                        deliveryStatus.StatusMessage += string.Format("\r\n{0}: {1}", match3.Groups[1].Value,
                            match3.Groups[5].Value.Trim());
                    }
                    handled = true;
                    handlerCount++;
                    MarkCurrentLogLineAsParsed();
                }
            }
        }

        private void ParseBounceMessage(string logLine, Mail mail)
        {
            var regex = new Regex(@"(T=""(.*?)"")? from <(.+)?> for (.+)");
            var match = regex.Match(logLine);
            if (match.Success)
            {
                mail.RecipientAddress = match.Groups[4].Value.Trim().ToLower();
                mail.SenderAddress = match.Groups[3].Value.Trim().ToLower();
                mail.Subject = ConvertSubject(match.Groups[2].Value);
                MarkCurrentLogLineAsParsed();
            }
            BounceMessages.Add(logLine);
        }

        private void ParseReceivedMessage(string logLine, Mail mail)
        {
            var handlerCount = 0;
            var regex1 = new Regex(@"H=(.+?) \[(.+?)\].+\sS=(.+?)\s");
            var match1 = regex1.Match(logLine);
            if (match1.Success)
            {
                handlerCount++;
                mail.SenderHostname = match1.Groups[1].Value;
                mail.SenderIpAddress = match1.Groups[2].Value;
                mail.Size = int.Parse(match1.Groups[3].Value);
            }
            var regex2 = new Regex(@"(T=""(.*?)"")? from <(.+)?> for (.+)");
            var match2 = regex2.Match(logLine);
            if (match2.Success)
            {
                handlerCount++;
                mail.RecipientAddress = match2.Groups[4].Value.Trim().ToLower();
                mail.SenderAddress = match2.Groups[3].Value.Trim().ToLower();
                mail.Subject = ConvertSubject(match2.Groups[2].Value);
            }
            if (handlerCount == 2)
            {
                MarkCurrentLogLineAsParsed();
            }
        }

        private void ParseOutputMessage(string logLine, Mail mail)
        {
            var handled = false;
            var handlerCount = 0;
            var regex1 =
                new Regex(
                    @"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) (=>|->) ([^\s]+)\s<(.+?)> F=<");
            var match1 = regex1.Match(logLine);
            if (match1.Success)
            {
                handlerCount++;
                var deliveryStatus = new DeliveryStatus();
                mail.DeliveryStatus.Add(deliveryStatus);
                deliveryStatus.Delivered = true;
                deliveryStatus.MailId = mail.Id;
                deliveryStatus.RecipientAddress = match1.Groups[6].Value;
                deliveryStatus.Time = DateTime.Parse(match1.Groups[1].Value);
                ParseDeliveryTerm(deliveryStatus, logLine);
                handled = true;
            }
            if (handled == false)
            {
                var regex2 =
                    new Regex(
                        @"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) (=>|->) (.+?) \((.+)\) <(.+?)> F=");
                var match2 = regex2.Match(logLine);
                if (match2.Success)
                {
                    handlerCount++;
                    var deliveryStatus = new DeliveryStatus();
                    mail.DeliveryStatus.Add(deliveryStatus);
                    deliveryStatus.Delivered = true;
                    deliveryStatus.MailId = mail.Id;
                    deliveryStatus.RecipientAddress = match2.Groups[6].Value;
                    deliveryStatus.Time = DateTime.Parse(match2.Groups[1].Value);
                    ParseDeliveryTerm(deliveryStatus, logLine);
                    handled = true;
                }
            }
            if (handled == false)
            {
                var regex =
                    new Regex(
                        @"(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) \[(\d.+?)] (.{6}-.{6}-.{2}) (=>|->) ([^\s]+) F=<.+C=""(.+?)""");
                var match = regex.Match(logLine);
                if (match.Success)
                {
                    var deliveryStatus = new DeliveryStatus();
                    mail.DeliveryStatus.Add(deliveryStatus);
                    deliveryStatus.MailId = mail.Id;
                    deliveryStatus.RecipientAddress = match.Groups[5].Value.Trim().ToLower();
                    deliveryStatus.StatusMessage = match.Groups[6].Value.Trim();
                    deliveryStatus.Time = DateTime.Parse(match.Groups[1].Value);
                    if (deliveryStatus.StatusMessage.StartsWith("250"))
                    {
                        deliveryStatus.Delivered = true;
                    }
                    ParseDeliveryTerm(deliveryStatus, logLine);
                    handled = true;
                }
            }
            if (handled)
            {
                MarkCurrentLogLineAsParsed();
            }
            if (handlerCount > 1)
            {
            }
        }

        private void ParseDeliveryTerm(DeliveryStatus deliveryStatus, string logLine)
        {
            var regex = new Regex(@"QT=(.+?)\sDT=(.+)");
            var match = regex.Match(logLine);
            if (match.Success)
            {
                deliveryStatus.QueryTime = match.Groups[1].Value;
                deliveryStatus.DeliveryTime = match.Groups[2].Value;
            }
        }

        private string ConvertSubject(string line)
        {
            var convertedLine = line;
            var regex1 = new Regex(@"\\\d{3}\\\d{3}");
            var match1 = regex1.Matches(line);
            foreach (Match item in match1)
            {
                if (item.Success)
                {
                    var newChar = ConvertEscapedChar(item.Value);
                    convertedLine = convertedLine.Replace(item.Value, newChar);
                }
            }
            return convertedLine;
        }

        private string ConvertEscapedChar(string escapedChar)
        {
            var bytes = escapedChar.Split(new[] {'\\'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => (byte) Convert.ToInt32(s, 8))
                .ToArray();
            return Encoding.UTF8.GetString(bytes);
        }

        private void MarkCurrentLogLineAsParsed()
        {
            if (!_parsedLogLines.ContainsKey(_currentLogLineNumber))
            {
                _parsedLogLines.Add(_currentLogLineNumber, 1);
            }
        }
    }
}