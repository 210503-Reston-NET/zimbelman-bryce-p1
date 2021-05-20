using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface IOrderBL
    {
        /// <summary>
        /// Business Logic to retrieve a list of a specific customers orders
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        List<Order> GetCustomerOrders(int customerId);
         /// <summary>
         /// Business Logic to retrieve a list of a specifc locations orders
         /// </summary>
         /// <param name="locationId"></param>
         /// <returns></returns>
        List<Order> GetLocationOrders(int locationId);
         /// <summary>
         /// Business Logic to add an order
         /// </summary>
         /// <param name="order"></param>
         /// <param name="location"></param>
         /// <param name="customer"></param>
         /// <returns></returns>
        Order AddOrder(Order order, Location location, Customer customer);
         /// <summary>
         /// Business Logic to update an order
         /// </summary>
         /// <param name="order"></param>
         /// <param name="location"></param>
         /// <param name="customer"></param>
         /// <returns></returns>
        Order UpdateOrder(Order order, Location location, Customer customer);
        /// <summary>
        /// Business Logic to view a specific customer order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Order ViewOrder(int orderId);
         /// <summary>
         /// Business Logic to retrieve all orders
         /// </summary>
         /// <returns></returns>
        List<Order> GetAllOrders();
        /// <summary>
        /// Business Logic to delete an order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order DeleteOrder(Order order);
    }
}