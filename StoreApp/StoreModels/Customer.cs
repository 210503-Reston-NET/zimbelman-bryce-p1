using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace StoreModels
{
    /// <summary>
    /// Customer Model
    /// </summary>
    public class Customer
    {
        public Customer(string firstName, string lastName, string birthdate, string phoneNumber, string email, string mailAddress) {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthdate = birthdate;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.MailAddress = mailAddress;
        }

        public Customer() {
            
        }

        public Customer(int id, string firstName, string lastName, string birthdate, string phoneNumber, string email, string mailAddress) : this(firstName, lastName, birthdate, phoneNumber, email, mailAddress) {
            this.Id = id;
        }

        /// <summary>
        /// This represents a unique value for every customer
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// This represents the first name of the customer
        /// </summary>
        /// <value></value>
        public string FirstName { get; set; }

        /// <summary>
        /// This represents the last name of the customer
        /// </summary>
        /// <value></value>
        public string LastName { get; set; }

        /// <summary>
        /// This represents the customers birthdate
        /// </summary>
        /// <value></value>
        public string Birthdate { get; set; }

        /// <summary>
        /// This represents the customer's phone number
        /// </summary>
        /// <value></value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// This represents the customer's email
        /// </summary>
        /// <value></value>
        public string Email { get; set; }

        /// <summary>
        /// This represents the customer's mailing adress
        /// </summary>
        /// <value></value>
        public string MailAddress { get; set; }
        public int CustomerID { get; set; }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName} \nBirthdate: {Birthdate} \nPhone Number: {PhoneNumber} \nEmail: {Email} \nMailing Address: {MailAddress} \n";
        }

        public bool Equals(Customer customer) {
            return this.FirstName.Equals(customer.FirstName) && this.LastName.Equals(customer.LastName) && this.Birthdate.Equals(customer.Birthdate);
        }
    }
}