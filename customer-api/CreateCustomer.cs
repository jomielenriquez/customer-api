using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using customer_api.Models;
using Microsoft.Azure.Cosmos;
using Container = Microsoft.Azure.Cosmos.Container;
using Microsoft.Extensions.Configuration;

namespace customer_api
{
    public class CreateCustomer
    {
        private readonly ILogger<CreateCustomer> _logger;
        private readonly ICustomerService _customerService;

        public CreateCustomer(ILogger<CreateCustomer> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [FunctionName("CreateCustomer")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Customer")] HttpRequest req)
        {
            //_logger.LogInformation("C# HTTP trigger function processed a request.");
            string name = req.Query["name"];
            Customer customer = new Customer
            {
                Id = req.Query["firstname"] + req.Query["id"],
                PartitionKey = req.Query["id"],
                FirstName = req.Query["firstname"],
                LastName = req.Query["lastname"],
                BirthdayInEpoch = req.Query["birthday"],
                Email = req.Query["email"]
            };

            string response = await _customerService.CreateCustomer(customer);

            return new OkObjectResult(response);
        }
    }
}
