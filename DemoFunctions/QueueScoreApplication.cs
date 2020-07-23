using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DemoFunctions
{
    public static class QueueScoreApplication
    {
        //QueueTrigger
        [FunctionName("QueueScoreApplication")]
        public static void Run([QueueTrigger("loan-applications", Connection = "")]LoanApplication application,
            [Blob("accepted-applications/{rand-guid}")] out string acceptedApplication,
            [Blob("declined-applications/{rand-guid}")] out string declinedApplication,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {application.Name}");
            bool isAccepted = application.Age >= 18;

            if (isAccepted)
            {
                acceptedApplication = JsonConvert.SerializeObject(application);
                declinedApplication = null;
            }
            else
            {
                declinedApplication = JsonConvert.SerializeObject(application);
                acceptedApplication = null;
            }
        }
    }
}
