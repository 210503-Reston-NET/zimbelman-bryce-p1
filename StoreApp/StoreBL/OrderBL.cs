using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using System.Linq;
using Serilog;

namespace StoreBL
{
    public class OrderBL : IOrderBL
    {
        /// <summary>
        /// Business logic class for the order model
        /// </summary>
        
        private IRepository _repo;
        public OrderBL(IRepository repo) {
            _repo = repo;
        }
        public Order AddOrder(Order order, Location location, Customer customer)
        {
            Log.Information("BL sent order to DL");
            return _repo.AddOrder(order, location, customer);
        }

        public Order UpdateOrder(Order order, Location location, Customer customer) {
            Log.Information("BL sent order to DL");
            return _repo.UpdateOrder(order, location, customer);
        }

        public List<Order> GetAllOrders()
        {
            Log.Information("BL attempt to retrieve list of all orders from DL");
            return _repo.GetAllOrders();
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            List<Order> orders = _repo.GetAllOrders();
            List<Order> customerOrders = new List<Order>();
            foreach (Order order in orders)
            {
                if (customerId.Equals(order.CustomerID)) {
                    customerOrders.Add(order);
                }
            }
            if (customerOrders.Any()) {
                Log.Information("BL sent list of customer orders to UI");
                return customerOrders;
            } else {
                Log.Information("No matching orders found");
                throw new Exception("No matching orders found");
            }
        }

        public List<Order> GetLocationOrders(int locationId) {
            List<Order> orders = _repo.GetAllOrders();
            List<Order> locationOrders = new List<Order>();
            foreach (Order order in orders)
            {
                if (locationId.Equals(order.LocationID)) {
                    locationOrders.Add(order);
                }
            }
            if (locationOrders.Any()) {
                Log.Information("BL sent list of locations orders to UI");
                return locationOrders;
            } else {
                Log.Information("No matching orders found");
                throw new Exception("No matching orders found");
            }
        }

        public Order ViewOrder(int orderId)
        {
            List<Order> orders = _repo.GetAllOrders();
            foreach (Order order in orders) {
                if (orderId.Equals(order.OrderID)) {
                    Log.Information("BL sent order to UI");
                    return order;
                }
            }
            Log.Information("No matching orders found");
            throw new Exception("No matching orders found");
        }

        public Order DeleteOrder(Order order)
        {
            Log.Information("BL sent order to DL for deletion");
            return _repo.DeleteOrder(order);
        }
    }
}