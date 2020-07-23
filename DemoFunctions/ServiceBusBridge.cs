using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DemoFunctions
{
    public static class ServiceBusBridge
    {
        [FunctionName("ServiceBusBridge")]
        public static void Run([ServiceBusTrigger("loan-appliations", Connection = "")]string applicationJson,
                        [Queue("accepted-applications/{rand-guid}")] out LoanApplication application,

            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {applicationJson }");
            application = JsonConvert.DeserializeObject<LoanApplication>(applicationJson);
        }
    } 
}
