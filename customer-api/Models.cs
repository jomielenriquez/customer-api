using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace customer_api.Models
{
    public class Customer
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "customerid")]
        public string PartitionKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthdayInEpoch { get; set; }
        public string Email { get; set; }
    }
}
