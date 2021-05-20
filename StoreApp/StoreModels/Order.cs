using System;
using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// Order Model
    /// </summary>
    public class Order
    {
        public Order(int locationId, int customerId, int orderID, double total, string orderDate) {
            this.LocationID = locationId;
            this.CustomerID = customerId;
            this.OrderID = orderID;
            this.Total = total;
            this.OrderDate = orderDate;
        }

        public Order() {
            
        }

        public Order(int id, int locationId, int customerId, int orderID, double total, string orderDate) : this(locationId, customerId, orderID, total, orderDate) {
            this.Id = id;
        }

        /// <summary>
        /// This represents a unqie value for every order
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// This represents the customer information of an order
        /// </summary>
        /// <value></value>
        public int CustomerID { get; set; }

        /// <summary>
        /// This represents the location information of an order
        /// </summary>
        /// <value></value>
        public int LocationID { get; set; }
        
        /// <summary>
        /// This represents a unique value for every order
        /// </summary>
        /// <value></value>
        public int OrderID { get; set; }

        /// <summary>
        /// This represents the total dollar amount of an order
        /// </summary>
        /// <value></value>
        public double Total { get; set; }
        public string OrderDate { get; set; }
    }
}