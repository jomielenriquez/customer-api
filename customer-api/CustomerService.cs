using System;
using System.Collections.Generic;
using System.Text;
using customer_api.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace customer_api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly string _EndpointUri;
        private readonly string _PrimaryKey;
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;
        private string _databaseId;
        private string _containerId;

        public CustomerService(IConfiguration configuration) 
        {
            _EndpointUri = configuration["EndPointUri"];
            _PrimaryKey = configuration["PrimaryKey"];
            _databaseId = configuration["DatabaseID"];
            _containerId = configuration["ContainerID"];
        }
        public async Task<string> CreateCustomer(Customer customer)
        {
            // Create a new instance of the Cosmos Client
            this.cosmosClient = new CosmosClient(_EndpointUri, _PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
            this.database = this.cosmosClient.GetDatabase(_databaseId);
            this.container = this.database.GetContainer(_containerId);

            try
            {
                ItemResponse<Customer> testing = await this.container.CreateItemAsync<Customer>(customer, new PartitionKey(customer.PartitionKey));
                return "Successfully Saved";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
