using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using StoreModels;

namespace StoreWebUI.Models
{
    public class CustomerVM
    {
        /// <summary>
		/// Customer View Model
		/// </summary>
        public CustomerVM() {


        }

        public CustomerVM(Customer customer)
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Birthdate = customer.Birthdate;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            MailAddress = customer.MailAddress;
        }

        /// <summary>
        /// This represents a unique value for every customer
        /// </summary>
        /// <value></value>
        public int CustomerID { get; set; }
        /// <summary>
        /// This represents the first name of the customer
        /// </summary>
        /// <value></value>
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// This represents the last name of the customer
        /// </summary>
        /// <value></value>
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// This represents the customers birthdate
        /// </summary>
        /// <value></value>
        [Required]
        public string Birthdate { get; set; }

        /// <summary>
        /// This represents the customer's phone number
        /// </summary>
        /// <value></value>
        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// This represents the customer's email
        /// </summary>
        /// <value></value>
        [Required]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        /// <summary>
        /// This represents the customer's mailing adress
        /// </summary>
        /// <value></value>
        [Required]
        [DisplayName("Mailing Address")]
        public string MailAddress { get; set; }
    }
}