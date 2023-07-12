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
        //Task<string> InsertCustomer(Customer customer);

        ///// <summary>
        ///// Get a book by its id from the Books collection
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<Customer> GetBook(string id);

        ///// <summary>
        ///// Insert a book into the Books collection
        ///// </summary>
        ///// <param name="book"></param>
        ///// <returns></returns>
        Task<string> CreateCustomer(Customer customer);

        ///// <summary>
        ///// Updates an existing book in the Books collection
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="book"></param>
        ///// <returns></returns>
        //Task UpdateBook(string id, Book bookIn);

        ///// <summary>
        ///// Removes a book from the Books collection
        ///// </summary>
        ///// <param name="book"></param>
        ///// <returns></returns>
        //Task RemoveBook(Book bookIn);

        ///// <summary>
        ///// Removes a book with the specified id from the Books collection
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task RemoveBookById(string id);
    }
}
