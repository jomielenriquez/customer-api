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
using MongoDB.Bson;

namespace customer_api
{
    public class UpdateCustomer
    {
        private readonly ILogger<CreateCustomer> _logger;
        private readonly ICustomerService _customerService;

        public UpdateCustomer(ILogger<CreateCustomer> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [FunctionName("UpdateCustomer")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdateCustomer")] HttpRequest req)
        {
            var incomingRequest = await new StreamReader(req.Body).ReadToEndAsync();
            var bookRequest = JsonConvert.DeserializeObject<Customer>(incomingRequest);

            long epochTimestamp = 0;

            if (bookRequest.BirthdayInEpoch != null)
            {
                DateTime enteredDate = DateTime.Parse(bookRequest.BirthdayInEpoch);

                DateTimeOffset dateTimeOffset = new DateTimeOffset(enteredDate);
                epochTimestamp = dateTimeOffset.ToUnixTimeSeconds();
            }


            Customer customer = new Customer
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FirstName = bookRequest.FirstName,
                LastName = bookRequest.LastName,
                BirthdayInEpoch = (bookRequest.BirthdayInEpoch==null ? "" : epochTimestamp.ToString()),
                Email = bookRequest.Email,
            };

            string response = await _customerService.UpdateCustomer(req.Query["id"], customer);

            return new OkObjectResult(response);
        }
    }
}
