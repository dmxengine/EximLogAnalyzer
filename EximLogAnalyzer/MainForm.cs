using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using EximLogAnalyzer.Models;
using Renci.SshNet;
using Yandex.Metrica;

namespace EximLogAnalyzer
{
    public partial class MainForm : Form
    {
        private SshClient _sshClient;
        private Stream _currentLogStream;
        private LogParser _logParser = new LogParser();
        private Config _config;

        public MainForm()
        {
            InitializeComponent();
            YandexMetrica.Activate("eafbf946-7082-4f83-aef3-6e99c1911a62");
            AppDomain.CurrentDomain.UnhandledException += Application_ThreadException;
        }

        private void Application_ThreadException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception) e.ExceptionObject;
            YandexMetrica.ReportError(ex.Message, ex);
            MessageBox.Show(ex.StackTrace,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void DisplayResult()
        {
            ArrayList al = new ArrayList();

            foreach (KeyValuePair<string, Mail> value in _logParser.ResultDict)
            {
                Mail mail = value.Value;
                if (mail.DeliveryStatus.Count == 0)
                {
                    Data data = new Data();
                    data.Id = mail.Id;
                    data.SenderAddress = mail.SenderAddress;
                    data.SenderHostname = mail.SenderHostname;
                    data.SenderIpAddress = mail.SenderIpAddress;
                    data.Completed = mail.Completed.ToString();
                    data.StartTime = mail.StartTime.ToString();
                    data.EndTime = mail.EndTime.ToString();
                    data.Subject = mail.Subject;
                    data.Delivered = "True/Manual";
                    data.RecipientAddress = mail.RecipientAddress;
                    data.Size = mail.Size.ToString();
                    al.Add(data);
                }
                foreach (DeliveryStatus status in mail.DeliveryStatus)
                {
                    Data data = new Data();
                    data.Id = mail.Id;
                    data.SenderAddress = mail.SenderAddress;
                    data.SenderHostname = mail.SenderHostname;
                    data.SenderIpAddress = mail.SenderIpAddress;
                    data.Completed = mail.Completed.ToString();
                    data.Deferred = status.Deferred.ToString();
                    data.StartTime = mail.StartTime.ToString();
                    data.EndTime = mail.EndTime.ToString();
                    data.Subject = mail.Subject;
                    data.Delivered = status.Delivered.ToString();
                    data.RecipientAddress = status.RecipientAddress;
                    data.QueryTime = status.QueryTime;
                    data.DeliveryTime = status.DeliveryTime;
                    data.Status = status.StatusMessage;
                    data.DeliveredTime = status.Time.ToString();
                    data.Size = mail.Size.ToString();
                    al.Add(data);
                }
            }
            gridGroupingControl1.DataSource = al;
        }

        private void DisplayNotParsedLines()
        {
            notParsedLogLinesRichTextBox.Clear();
            foreach (var line in _logParser.NotParsedLogLines)
            {
                notParsedLogLinesRichTextBox.AppendText(line + "\r\n");
                notParsedLogLinesRichTextBox.AppendText("\r\n");
            }
        }

        private void DisplayMailBoxIsFull()
        {
            mailBoxIsFullRichTextBox.Clear();
            foreach (var line in _logParser.MailBoxIsFull)
            {
                mailBoxIsFullRichTextBox.AppendText(line + "\r\n");
                mailBoxIsFullRichTextBox.AppendText("\r\n");
            }
        }

        private void DisplayBounceMessages()
        {
            bounceMessagesRichTextBox.Clear();
            foreach (var line in _logParser.BounceMessages)
            {
                bounceMessagesRichTextBox.AppendText(line + "\r\n");
                bounceMessagesRichTextBox.AppendText("\r\n");
            }
        }

        private void DisplayDeliveryDeferredMessages()
        {
            DeliveryDeferredRichTextBox.Clear();
            foreach (var line in _logParser.DeliveryDeferred)
            {
                DeliveryDeferredRichTextBox.AppendText(line + "\r\n");
                DeliveryDeferredRichTextBox.AppendText("\r\n");
            }
        }

        private void DisplayDovecotLoginAuthenticatorFailed()
        {
            richTextBox2.Clear();
            foreach (var line in _logParser.DovecotAuthenticatorFailed)
            {
                richTextBox2.AppendText(line + "\r\n");
                richTextBox2.AppendText("\r\n");
            }
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            findInCurrentLogRichTextBox.Clear();
            if (_currentLogStream != null)
            {
                _currentLogStream.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(_currentLogStream);
                while (!sr.EndOfStream)
                {
                    string logLine = sr.ReadLine();
                    if (logLine!=null && logLine.Contains(textBox1.Text))
                    {
                        findInCurrentLogRichTextBox.AppendText(logLine + "\r\n\r\n");
                    }
                }
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (_sshClient == null || _sshClient.IsConnected == false)
            {
                _sshClient = new SshClient(hostnameTextBox.Text, loginTextBox.Text, passwordTextBox.Text);
                try
                {
                    _sshClient.Connect();
                    GetLogList(logPathTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            if (_sshClient.IsConnected)
            {
                logFilesComboBox.Items.Clear();
                _sshClient.Disconnect();
            }
        }

        private void GetLogList(string path)
        {
            path = path.TrimEnd('/');
            logFilesComboBox.Items.Clear();
            if (_sshClient != null && _sshClient.IsConnected)
            {
                try
                {
                    SshCommand sshCommand = _sshClient.CreateCommand(string.Format("ls {0}", path));
                    List<string> result = sshCommand.Execute().Split().ToList();
                    foreach (string item in result)
                    {
                        logFilesComboBox.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _currentLogStream = new MemoryStream();
                string logPath = logPathTextBox.Text.TrimEnd('/');
                SftpClient sftpClient = new SftpClient(hostnameTextBox.Text, loginTextBox.Text, passwordTextBox.Text);
                sftpClient.Connect();
                sftpClient.DownloadFile(string.Format("{0}/{1}", logPath, logFilesComboBox.Text), _currentLogStream);
                _currentLogStream.Seek(0, SeekOrigin.Begin);
                MemoryStream decompressedStream;
                if (logFilesComboBox.Text.Contains(".gz"))
                {
                    decompressedStream = DecompressStream(_currentLogStream);
                    _currentLogStream = decompressedStream;
                    _logParser.ParseEximMainLog(decompressedStream);
                }
                else
                {
                    _logParser.ParseEximMainLog(_currentLogStream);
                }
                DisplayResult();
                DisplayMailBoxIsFull();
                DisplayNotParsedLines();
                DisplayBounceMessages();
                DisplayDovecotLoginAuthenticatorFailed();
                DisplayDeliveryDeferredMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private MemoryStream DecompressStream(Stream inputStream)
        {
            GZipStream stream = new GZipStream(inputStream, CompressionMode.Decompress);
            const int size = 4096;
            byte[] buffer = new byte[size];
            MemoryStream memory = new MemoryStream();
            int count;
            do
            {
                count = stream.Read(buffer, 0, size);
                if (count > 0)
                {
                    memory.Write(buffer, 0, count);
                }
            }
            while (count > 0);
            memory.Seek(0, SeekOrigin.Begin);
            return memory;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_sshClient == null || _sshClient.IsConnected == false)
            {
                connectButton.Text = "Connect";
                refreshButton.Enabled = false;
            }

            if (_sshClient != null && _sshClient.IsConnected)
            {
                connectButton.Text = "Disconnect";
                refreshButton.Enabled = true;
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            GetLogList(logPathTextBox.Text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _config = new Config();
            if (File.Exists("Config.xml"))
            {
                _config = Config.LoadConfig("Config.xml");
            }
            hostnameTextBox.Text = _config.Hostname;
            loginTextBox.Text = _config.Login;
            passwordTextBox.Text = _config.Password;
            logPathTextBox.Text = _config.LogPath;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _config.Hostname = hostnameTextBox.Text;
            _config.Login = loginTextBox.Text;
            _config.Password = passwordTextBox.Text;
            _config.LogPath = logPathTextBox.Text;
            try
            {
                Config.SaveConfig(_config, "Config.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
