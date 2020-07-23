using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusDemo.DTO;

namespace ServiceBusDemo.Topics
{
    public class TopicSender
    {

        static ITopicClient topicClient;

        public static void SendMessage(SenderRequest data)
        {
            Main(data).GetAwaiter().GetResult();
        }

        private static void CreateConnection(string connectionString, string topicName)
        {
            topicClient = new TopicClient(connectionString, topicName);
        }

        private static async Task Main(SenderRequest dataBody)
        {
            try
            {
                CreateConnection(dataBody.ConnectionString, dataBody.TopicName);
                // Create a new message to send to the topic
                var message = new Message(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dataBody.Messages)));
                // Write the body of the message to the console
                // Send the message to the topic
                await topicClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                throw;
            }
            finally
            {
                await topicClient.CloseAsync();
            }
        }
    }

}
