using System;

namespace EximLogAnalyzer.Models
{
    public class DeliveryStatus
    {
        public string MailId { get; set; }
        public string RecipientAddress { get; set; }
        public string StatusMessage { get; set; }
        public bool Delivered { get; set; }
        public bool Deferred { get; set; } // == delivery deferred; temporary problem
        public string QueryTime { get; set; }
        public string DeliveryTime { get; set; }
        public DateTime Time { get; set; }
    }
}
