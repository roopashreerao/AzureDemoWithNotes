using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceBusDemo.DTO;
using Microsoft.Azure.ServiceBus;

namespace ServiceBusDemo.Topics
{
    public class TopicReceiver
    {
        private static ISubscriptionClient subscriptionClient;

        public void CreateConnection(string connectionString, string topicName, string subscriptionName)
        {
            subscriptionClient = new SubscriptionClient(connectionString, topicName, subscriptionName, ReceiveMode.PeekLock);
        }

        public void ReceiveMessage(ClientRequest request)
        {
            ReceiveMessageFromAzureBus(request).GetAwaiter().GetResult();
        }

        private static async Task ReceiveMessageFromAzureBus(ClientRequest request)
        {
            try
            {
                //new CreateConnection(request.ConnectionString, request.TopicName, request.SubscriptionName);
                new TopicReceiver().RegisterOnMessageHandlerAndReceiveMessages(null);
                await subscriptionClient.CloseAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RegisterOnMessageHandlerAndReceiveMessages(Func<Message, CancellationToken, Task> function)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            if (function == null)
                function = ProcessMessagesAsync;
            subscriptionClient.RegisterMessageHandler(function, messageHandlerOptions);
        }

        static async Task<Message> ProcessMessagesAsync(Message message, CancellationToken token)
        {

            // Process the message.
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            // Complete the message so that it is not received again.
            // This can be done only if the subscriptionClient is created in ReceiveMode.PeekLock mode (which is the default).
            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            return message;
            // Note: Use the cancellationToken passed as necessary to determine if the subscriptionClient has already been closed.
            // If subscriptionClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc.
            // to avoid unnecessary exceptions.
        }

        public static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await subscriptionClient.CloseAsync();
        }
    }
}
