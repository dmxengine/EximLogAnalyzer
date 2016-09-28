using System;
using System.Collections.Generic;

namespace EximLogAnalyzer.Models
{
    public class Mail
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SenderAddress { get; set; }
        public string SenderHostname { get; set; }
        public string SenderIpAddress { get; set; }
        public string Subject { get; set; }
        public int Size{ get; set; }
        public bool Completed { get; set; }
        

        public string RecipientAddress { get; set; }
        public List<DeliveryStatus> DeliveryStatus { get; set; }

        public Mail()
        {
            DeliveryStatus = new List<DeliveryStatus>();
        }
    }
}
