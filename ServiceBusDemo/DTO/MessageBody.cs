using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusDemo.DTO
{
    public class ConnectionInfo
    {
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
    }

    public class SenderRequest : ConnectionInfo
    {
        public string Messages { get; set; }
    }

    public class ClientRequest : ConnectionInfo
    {
        public string SubscriptionName { get; set; }
    }
}
