using System;
using System.Collections.Generic;
using System.Text;
using customer_api.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using LinqToDB;

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

        public async Task<List<Customer>> GetAllCustomer()
        {
            var sqlQueryText = "SELECT * FROM c";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Customer> queryResultSetIterator = this.container.GetItemQueryIterator<Customer>(queryDefinition);

            List<Customer> customers = new List<Customer>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Customer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Customer customer in currentResultSet)
                {
                    customers.Add(customer);
                }
            }
            return customers;
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
        public async Task<string> UpdateCustomer(string id, Customer customer)
        {
            try
            {
                var response = await GetCustomerById(id);

                ItemResponse<Customer> wakefieldFamilyResponse = await this.container.ReadItemAsync<Customer>(id, new PartitionKey(response.FirstName));
                var itemBody = wakefieldFamilyResponse.Resource;

                if(customer.FirstName != null)
                {
                    return "Cannot Update First Name";
                }
                if(customer.LastName != null)
                {
                    itemBody.LastName = customer.LastName;
                }
                if(customer.BirthdayInEpoch != null)
                {
                    itemBody.BirthdayInEpoch = customer.BirthdayInEpoch;
                }
                if(customer.Email != null)
                {
                    itemBody.Email = customer.Email;
                }

                wakefieldFamilyResponse = await this.container.ReplaceItemAsync<Customer>(itemBody, itemBody.Id, new PartitionKey(itemBody.FirstName));

                return "Successfully updated";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
