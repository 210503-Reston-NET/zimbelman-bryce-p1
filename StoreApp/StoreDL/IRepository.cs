using System.Collections.Generic;
using StoreModels;
namespace StoreDL
{
    public interface IRepository
    {
        /// <summary>
        /// Data Logic to retrieve a list of all customers
        /// </summary>
        /// <returns></returns>
        List<Customer> GetAllCustomers();
        /// <summary>
        /// Data Logic to add a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Customer AddCustomer(Customer customer);
        /// <summary>
        /// Data Logic to retrieve a specific customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Customer GetCustomer(Customer customer);
        /// <summary>
        /// Data Logic to add a location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        Location AddLocation(Location location);
        /// <summary>
        /// Data Logic to retreive a specific location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        Location GetLocation(Location location);
        /// <summary>
        /// Data Logic to retrieve a list of all locations
        /// </summary>
        /// <returns></returns>
        List<Location> GetAllLocations();
        /// <summary>
        /// Data Logic to add a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product AddProduct(Product product);
        /// <summary>
        /// Data Logic to retrieve a specific product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product GetProduct(Product product);
        /// <summary>
        /// Data Logic to retrieve a list of all products
        /// </summary>
        /// <returns></returns>
        List<Product> GetAllProducts();
        /// <summary>
        /// Data Logic to add a store inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="location"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Inventory AddInventory(Inventory inventory, Location location, Product product);
        /// <summary>
        /// Data Logic to update a store inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="location"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Inventory UpdateInventory(Inventory inventory, Location location, Product product);
        /// <summary>
        /// Data Logic to retrieve a list of all inventories
        /// </summary>
        /// <returns></returns>
        List<Inventory> GetAllInventories();
        /// <summary>
        /// Data Logic to retrieve a specific store inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        Inventory GetStoreInventory(Inventory inventory);
        /// <summary>
        /// Data Logic to add an order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="location"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        Order AddOrder(Order order, Location location, Customer customer);
        /// <summary>
        /// Data Logic to update an order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="location"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        Order UpdateOrder(Order order, Location location, Customer customer);
        /// <summary>
        /// Data Logic to retrieve a specific order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// 
        /// <summary>
        /// Data Logic to delete a specific order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order DeleteOrder(Order order);
        Order GetOrder(Order order);
        /// <summary>
        /// Data Logic to retrieve a list of all orders
        /// </summary>
        /// <returns></returns>
        List<Order> GetAllOrders();
        /// <summary>
        /// Data Logic to retrieve a list of all line items
        /// </summary>
        /// <returns></returns>
        List<LineItem> GetAllLineItems();
        /// <summary>
        /// Data Logic to add a line item
        /// </summary>
        /// <param name="lineItem"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        LineItem AddLineItem(LineItem lineItem, Product product);
        /// <summary>
        /// Data Logic to retrieve a specific line item
        /// </summary>
        /// <param name="lineItem"></param>
        /// <returns></returns>
        LineItem GetLineItem(LineItem lineItem);
        /// <summary>
        /// Data Logic to delete a specific line item
        /// </summary>
        /// <param name="lineItem"></param>
        /// <returns></returns>
        LineItem DeleteLineItem(LineItem lineItem);
    }
}