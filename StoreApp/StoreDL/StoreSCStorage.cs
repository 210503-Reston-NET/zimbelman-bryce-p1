using System.Collections.Generic;
using StoreModels;
using System;
namespace StoreDL
{
    /// <summary>
    /// Mocha Moment static collection storage
    /// </summary>
    public class StoreSCStorage
    {
        /// <summary>
        /// Static collection storage of customers
        /// </summary>
        /// <typeparam name="Customer"></typeparam>
        /// <returns></returns>
        public static List<Customer> Customers = new List<Customer>() {
            new Customer("firstName", "lastName","birthdate", "phone#", "email", "Mailing Address")
        };
        /// <summary>
        /// Static collection storage of locations
        /// </summary>
        /// <typeparam name="Location"></typeparam>
        /// <returns></returns>
        public static List<Location> Locations = new List<Location>() {
            new Location("name", "address", "city", "state")
        };
        /// <summary>
        /// Static collection storage of products
        /// </summary>
        /// <typeparam name="Product"></typeparam>
        /// <returns></returns>
        public static List<Product> Products = new List<Product>() {
            new Product("name", 1.99, "description")
        };
        /// <summary>
        /// Static collection storage of orders
        /// </summary>
        /// <typeparam name="Order"></typeparam>
        /// <returns></returns>
        public static List<Order> Orders = new List<Order>() {
            new Order(1, 1, 1, 1.99, "")
        };
        /// <summary>
        /// Static collection storage of line items
        /// </summary>
        /// <typeparam name="LineItem"></typeparam>
        /// <returns></returns>
        public static List<LineItem> LineItems = new List<LineItem>() {
            new LineItem(1, 1, 1)
        };
        /// <summary>
        /// Static collection storage of inventories
        /// </summary>
        /// <typeparam name="Inventory"></typeparam>
        /// <returns></returns>
        public static List<Inventory> Inventories = new List<Inventory>() {
            new Inventory(1, 1, 1)
        };
    }
}