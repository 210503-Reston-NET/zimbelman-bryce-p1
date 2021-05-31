using System;
using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// Order Model
    /// </summary>
    public class Order
    {
        public Order(int customerId, int locationId, double total, string orderDate) {
            this.LocationID = locationId;
            this.CustomerID = customerId;
            this.Total = total;
            this.OrderDate = orderDate;
        }

        public Order() {
            
        }

        public Order(int orderId, int locationId, int customerId, double total, string orderDate) : this(locationId, customerId, total, orderDate) {
            this.OrderID = orderId;
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
        public int CustomerID { get; set; }

        /// <summary>
        /// This represents the location information of an order
        /// </summary>
        /// <value></value>
        public int LocationID { get; set; }

        /// <summary>
        /// This represents the total dollar amount of an order
        /// </summary>
        /// <value></value>
        public double Total { get; set; }
        /// <summary>
        /// This represents the date an order was placed
        /// </summary>
        public string OrderDate { get; set; }

        public List<LineItem> LineItems { get; set; }
    }
}