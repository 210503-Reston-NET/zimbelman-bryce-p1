using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface ICustomerBL
    {
        /// <summary>
        /// Business Logic to retrieve a list of all customers
        /// </summary>
        /// <returns></returns>
        List<Customer> GetAllCustomers();
        /// <summary>
        /// Business Logic to add a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Customer AddCustomer(Customer customer);
        /// <summary>
        /// Business Logic to search for a specific customer
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        Customer SearchCustomer(string firstName, string lastName);
         /// <summary>
        /// Business Logic to search for a specific customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Customer SearchCustomer(int customerId);
    }
}