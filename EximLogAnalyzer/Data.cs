namespace EximLogAnalyzer
{
    internal class Data
    {
        public string Id { get; set; }
        public string StartTime { get; set; }
        public string SenderAddress { get; set; }
        public string Subject { get; set; }
        public string RecipientAddress { get; set; }
        public string Status { get; set; }
        public string SenderHostname { get; set; }
        public string SenderIpAddress { get; set; }
        
        public string Size { get; set; }
        public string Completed { get; set; }
        public string Delivered { get; set; } // ** delivery failed; address bounced
        public string Deferred { get; set; } // == delivery deferred; temporary problem
        public string EndTime { get; set; }

        
        public string QueryTime { get; set; }
        public string DeliveryTime { get; set; }
        public string DeliveredTime { get; set; }
        
    }
}