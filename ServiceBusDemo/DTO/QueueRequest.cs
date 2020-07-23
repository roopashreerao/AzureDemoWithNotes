using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusDemo.DTO
{
    public class QueueRequest
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }

        public string Messages { get; set; }

        public Func<Message, CancellationToken, Task> CallbackFunction { get; set; }

    }
}
