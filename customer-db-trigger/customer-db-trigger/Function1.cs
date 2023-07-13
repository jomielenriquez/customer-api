using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace customer_db_trigger
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([CosmosDBTrigger(
            databaseName: "customerdb",
            collectionName: "customer",
            ConnectionStringSetting = "AccountEndpoint=https://0a0244a8-0ee0-4-231-b9ee.documents.azure.com:443/;AccountKey=2kivggeWXohzHuB9pRGIfFlFN6IjA2qzYvblUogn3sprNrMH2EBTMG5u5Bdzz8sP5BP5szy257VCACDb9OoePg==;",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
