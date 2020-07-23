using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace TableStorageDemo
{
    class Program
    {
        static CloudStorageAccount storageAccount;
        static CloudTableClient tableClient;
        static CloudTable employees;
        
        static void Main(string[] args)
        {
            storageAccount = Common.GetCloudStorageAccount();
            tableClient = storageAccount.CreateCloudTableClient();

            EmployeeTableEntity.EmployeeTest();

            Console.WriteLine("\n\n");
            Console.WriteLine("Press any key to end");
            Console.ReadKey();
        }

        public class EmployeeTableEntity : TableEntity
        {
            public EmployeeTableEntity() { }

            public EmployeeTableEntity(string firstName, string lastName, int age, string partition)
            {
                this.PartitionKey = partition;
                this.RowKey = firstName + lastName + age;
            }

            public static void EmployeeTest()
            {
                employees = tableClient.GetTableReference("Employees");

                //InsertOp("Roopashree", "Rao", 35);
                //InsertOp("Vivek", "Kumar", 30);
                //InsertOp("Aishwarya", "Shetty", 25);
                //InsertOp("Basu", "L", 27, "QA");
                //InsertOp("Aravind", "R", 25, "QA");
                QueryOp();
            }

            public static void InsertOp(string firstName, string lastName, int age, string partition = "Dev")
            {
                EmployeeTableEntity employeeItem = new EmployeeTableEntity(firstName, lastName, age, partition);
                TableOperation insertOp = TableOperation.Insert(employeeItem);
                employees.Execute(insertOp);
            }

            public static void QueryOp()
            {
                TableQuery<EmployeeTableEntity> query = new TableQuery<EmployeeTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Dev"));
                Console.WriteLine("List of all Dev Employees");
                Console.WriteLine("--------------------------------");
                foreach (EmployeeTableEntity emp in employees.ExecuteQuery(query))
                {
                    Console.WriteLine(emp.RowKey);
                }
            }
        }
    }
}
