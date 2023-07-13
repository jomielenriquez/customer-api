using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using customer_api.Models;

namespace customer_api
{
    public interface ICustomerService
    {
        /// <summary>
        /// Get all customer info
        /// </summary>
        /// <returns></returns>
        Task<List<Customer>> GetAllCustomer();

        /// <summary>
        /// Get customer info by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Customer> GetCustomerById(string id);

        /// <summary>
        /// Insert a new customer info
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Task<string> CreateCustomer(Customer customer);

        /// <summary>
        /// Updates an existing customer information by its id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        Task<string> UpdateCustomer(string id, Customer customer);

        ///// <summary>
        ///// Removes a book from the Books collection
        ///// </summary>
        ///// <param name="book"></param>
        ///// <returns></returns>
        //Task RemoveBook(Book bookIn);

        /// <summary>
        /// Removes a book with the specified id from the Books collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> RemoveCustomerById(string id, string firstName);
    }
}
