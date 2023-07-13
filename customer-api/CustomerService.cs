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
            this.cosmosClient = new CosmosClient(_EndpointUri, _PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
            this.database = this.cosmosClient.GetDatabase(_databaseId);
            this.container = this.database.GetContainer(_containerId);
        }
        public async Task<string> CreateCustomer(Customer customer)
        {
            try
            {
                ItemResponse<Customer> createCustomerResponse = await this.container.CreateItemAsync<Customer>(customer, new PartitionKey(customer.FirstName));
                return "Successfully Saved";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public async Task<Customer> GetCustomerById(string id)
        {
            var sqlQueryText = "SELECT * FROM c WHERE c.id = '" + id + "'";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Customer> queryResultSetIterator = this.container.GetItemQueryIterator<Customer>(queryDefinition);

            List<Customer> customers = new List<Customer>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Customer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Customer customer in currentResultSet)
                {
                    customers.Add(customer);
                    Console.WriteLine("\tRead {0}\n", customer);
                }
            }
            return customers.FirstOrDefault();
        }

        public async Task<string> RemoveCustomerById(string customerId, string firstName)
        {
            try
            {
                ItemResponse<Customer> RemoveCustomerResponse = await this.container.DeleteItemAsync<Customer>(customerId, new PartitionKey(firstName));
                return "Successfully Removed";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
