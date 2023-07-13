using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceStack;

namespace customer_api
{
    public class GetAllCustomers
    {
        private readonly ILogger<CreateCustomer> _logger;
        private readonly ICustomerService _customerService;

        public GetAllCustomers(ILogger<CreateCustomer> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [FunctionName("GetAllCustomers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllCustomers")] HttpRequest req)
        {
            var response = await _customerService.GetAllCustomer();

            return new OkObjectResult(response);
        }
    }
}
