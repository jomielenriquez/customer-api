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
using ServiceStack;

namespace customer_api
{
    public class GetCustomerById
    {
        private readonly ILogger<CreateCustomer> _logger;
        private readonly ICustomerService _customerService;

        public GetCustomerById(ILogger<CreateCustomer> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [FunctionName(nameof(GetCustomerById))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetCustomerById")] HttpRequest req)
        {
            var response = await _customerService.GetCustomerById(req.Query["id"]);

            DateTimeOffset dateTimeOffset1 = DateTimeOffset.FromUnixTimeSeconds(response.BirthdayInEpoch.ToLong());
            DateTime dateTime = dateTimeOffset1.LocalDateTime;

            response.BirthdayInEpoch = dateTime.Date.ToString().Split(" ")[0];

            return new OkObjectResult(response);
        }
    }
}
