using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using Serilog;
namespace StoreBL
{
    /// <summary>
    /// Business Lgoic class for the customer model
    /// </summary>
    public class CustomerBL : ICustomerBL
    {
        private IRepository _repo;
        public CustomerBL(IRepository repo) {
            _repo = repo;
        }

        public Customer AddCustomer(Customer customer) {
            if (_repo.GetCustomer(customer) != null) {
                Log.Information("Customer already exists");
                throw new Exception ("Customer already exists");
            }
            Log.Information("BL request to add customer");
            return _repo.AddCustomer(customer);
        }

        public List<Customer> GetAllCustomers() {
            Log.Information("BL request to retrive all customers");
            return _repo.GetAllCustomers();
        }

        public Customer SearchCustomer(string firstName, string lastName)
        {
            List<Customer> customers = GetAllCustomers();
                if (customers.Count == 0) {
                    Log.Information("No customers found");
                    throw new Exception ("No customers found");
                } else {
                    foreach (Customer customer in customers) {
                    if (firstName.Equals(customer.FirstName) && lastName.Equals(customer.LastName)) {
                        Log.Information("BL sends customer to UI");
                        return customer;
                    }
                }
                Log.Information("No matching customer found");
                throw new Exception ("No matching customer found");
            }
        }

        public Customer SearchCustomer(int customerId) {
            List<Customer> customers = GetAllCustomers();
            if (customers.Count == 0) {
                Log.Information("No customers found");
                throw new Exception ("No customers found");
            } else {
                foreach (Customer customer in customers)
                {
                    if (customerId.Equals(customer.Id)) {
                        Log.Information("BL sends customer to UI");
                        return customer;
                    }
                }
                Log.Information("No matching customer found");
                throw new Exception ("No matching customer found");
            }
        }
    }
}