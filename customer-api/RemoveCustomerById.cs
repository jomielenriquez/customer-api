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

namespace customer_api
{
    public class RemoveCustomerById
    {
        private readonly ILogger<CreateCustomer> _logger;
        private readonly ICustomerService _customerService;

        public RemoveCustomerById(ILogger<CreateCustomer> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [FunctionName(nameof(RemoveCustomerById))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "RemoveCustomerById")] HttpRequest req)
        {
            var getCustomer = await _customerService.GetCustomerById(req.Query["id"]);

            if(getCustomer == null)
            {
                return new OkObjectResult("Customer does not Exist");
            }

            string response = await _customerService.RemoveCustomerById(req.Query["id"],getCustomer.FirstName);

            return new OkObjectResult(response);
        }
    }
}
