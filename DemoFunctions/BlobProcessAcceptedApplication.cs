using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DemoFunctions
{
    public static class BlobProcessAcceptedApplication
    {
        //Blob trigger example
        [FunctionName("BlobProcessAcceptedApplication")]
        public static void Run([BlobTrigger("accepted-applications/{name}", Connection = "")]string applcationJson, 
            string name,
            ILogger log)
        {
            LoanApplication application = JsonConvert.DeserializeObject<LoanApplication>(applcationJson);
            log.LogInformation($"ProcessAcceptedApplication Blob trigger function Processed blob \n Name:{application.Name} \n Age: {application.Age} ");
        }
    }
}
