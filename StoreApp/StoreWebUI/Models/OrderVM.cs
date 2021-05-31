using System;
using StoreModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace StoreWebUI.Models
{
    public class OrderVM
    {
        public OrderVM()
        {

        }

        public OrderVM(Order order)
        {
            LocationID = order.LocationID;
            OrderID = order.OrderID;
            CustomerID = order.CustomerID;
            Total = order.Total;
            OrderDate = order.OrderDate;
        }

        /// <summary>
        /// This represents a unqie value for every order
        /// </summary>
        /// <value></value>
        public int OrderID { get; set; }
        /// <summary>
        /// This represents the customer information of an order
        /// </summary>
        /// <value></value>
        [DisplayName("Customer")]
        public int CustomerID { get; set; }

        /// <summary>
        /// This represents the location information of an order
        /// </summary>
        /// <value></value>
        [DisplayName("Store Name")]
        public int LocationID { get; set; }

        /// <summary>
        /// This represents the total dollar amount of an order
        /// </summary>
        /// <value></value>
        public double Total { get; set; }
        /// <summary>
        /// This represents the date an order was placed
        /// </summary>
        [DisplayName("Order Date")]
        public string OrderDate { get; set; }
    }
}
