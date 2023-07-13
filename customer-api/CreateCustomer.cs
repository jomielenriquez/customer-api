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
using MongoDB.Bson;
using ServiceStack;
using ServiceStack.Text;
using Azure;

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

        [FunctionName(nameof(CreateCustomer))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateCustomer")] HttpRequest req)
        {
            string response = string.Empty;
            try
            {
                var incomingRequest = await new StreamReader(req.Body).ReadToEndAsync();
                var bookRequest = JsonConvert.DeserializeObject<Customer>(incomingRequest);

                DateTime enteredDate = DateTime.Parse(bookRequest.BirthdayInEpoch);

                DateTimeOffset dateTimeOffset = new DateTimeOffset(enteredDate);
                long epochTimestamp = dateTimeOffset.ToUnixTimeSeconds();

                Customer customer = new Customer
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    FirstName = bookRequest.FirstName,
                    LastName = bookRequest.LastName,
                    BirthdayInEpoch = epochTimestamp.ToString(),
                    Email = bookRequest.Email,
                };

                response = await _customerService.CreateCustomer(customer);
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex.Message);
            }

            return new OkObjectResult(response);
        }
    }
}
