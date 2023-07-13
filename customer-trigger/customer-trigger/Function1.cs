using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace customer_trigger
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public void Run([CosmosDBTrigger(
            databaseName: "customerdb",
            collectionName: "customer",
            ConnectionStringSetting = "AccountEndpoint=https://0a0244a8-0ee0-4-231-b9ee.documents.azure.com:443/;AccountKey=2kivggeWXohzHuB9pRGIfFlFN6IjA2qzYvblUogn3sprNrMH2EBTMG5u5Bdzz8sP5BP5szy257VCACDb9OoePg==;",
            LeaseCollectionName = "leases")] IReadOnlyList<MyDocument> input)
        {
            if (input != null && input.Count > 0)
            {
                _logger.LogInformation("Documents modified: " + input.Count);
                _logger.LogInformation("First document Id: " + input[0].Id);
            }
        }
    }

    public class MyDocument
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public bool Boolean { get; set; }
    }
}
