using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBL;
using StoreModels;
using StoreWebUI.Models;
using Serilog;

namespace StoreWebUI.Controllers
{
    public class OrderController : Controller
    {
        private IOrderBL _orderBL;
        private ICustomerBL _customerBL;
        private ILocationBL _locationBL;
        private IProductBL _productBL;
        private ILineItemBL _lineItemBL;
        private IInventoryBL _inventoryBL;

        public OrderController(IOrderBL orderBL, ICustomerBL customerBL, ILocationBL locationBL, IProductBL productBL, ILineItemBL lineItemBL, IInventoryBL inventoryBL)
        {
            _orderBL = orderBL;
            _customerBL = customerBL;
            _locationBL = locationBL;
            _productBL = productBL;
            _lineItemBL = lineItemBL;
            _inventoryBL = inventoryBL;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string firstName, string lastName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("UI attempt to retrieve customer");
                    if (_customerBL.SearchCustomer(firstName, lastName) != null)
                    {
                        TempData["firstName"] = firstName;
                        TempData["lastName"] = lastName;
                        Log.Information("Redirected to Order Controller: Location");
                        return RedirectToAction(nameof(Location));
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // Get
        public ActionResult Location()
        {
            string firstName = TempData["firstName"].ToString();
            TempData["firstName"] = firstName;
            string lastName = TempData["lastName"].ToString();
            TempData["lastName"] = lastName;
            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Location(string storeName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("UI attempt to retrieve customer");
                    Customer customer = _customerBL.SearchCustomer(TempData["firstName"].ToString(), TempData["lastName"].ToString());
                    Log.Information("UI attempt to retrieve location");
                    Location location = _locationBL.GetLocation(storeName);
                    int orderID = 0;
                    if (location == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    Order newOrder = new Order(customer.CustomerID, location.LocationID, 0, DateTime.Now.ToString());
                    Log.Information("UI sent new order to BL");
                    _orderBL.AddOrder(newOrder, location, customer);
                    Log.Information("UI attempt to retrieve list of orders");
                    List<Order> orders = _orderBL.GetAllOrders();
                    // Retrieves latest orderID
                    foreach (Order order in orders)
                    {
                        orderID = order.OrderID;
                    }
                    TempData["OrderID"] = orderID;
                    Log.Information("Redirected to Order Controller: LineItems");
                    return RedirectToAction(nameof(LineItems));
                }
                return RedirectToAction(nameof(Index));
            } catch
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        // Get
        public ActionResult LineItems()
        {
            try
            {
                int i = 0;
                Log.Information("UI attempt to retrieve list of products");
                List<ProductVM> products = _productBL.GetAllProducts().Select(prod => new ProductVM(prod)).ToList();
                foreach (ProductVM item in products)
                {
                    string itemName = "itemName" + i;
                    ViewData.Add(itemName, item.ItemName);
                    i++;
                }
                string orderId = TempData["OrderID"].ToString();
                TempData["OrderID"] = orderId;
                return View(products);
            } catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LineItems(IFormCollection collection)
        {
            try
            {
                Log.Information("UI attempt to retrieve list of products");
                List<Product> products = _productBL.GetAllProducts();
                List<int> quantity = new List<int>();
                foreach (Product item in products)
                {
                    if (String.IsNullOrWhiteSpace(collection[item.ItemName]))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                foreach (Product item in products)
                {
                    LineItem newLineItem = new LineItem(item.ProductID, Int32.Parse(collection[item.ItemName]), Int32.Parse(TempData["OrderID"].ToString()));
                    quantity.Add(Int32.Parse(collection[item.ItemName]));
                    Log.Information("UI sent new line item to BL");
                    _lineItemBL.AddLineItem(newLineItem, item);
                }
                Log.Information("UI request total form BL");
                double orderTotal = _productBL.GetTotal(quantity);
                TempData["OrderTotal"] = orderTotal.ToString();
                string orderId = TempData["OrderID"].ToString();
                TempData["OrderID"] = orderId;
                TempData["Quantity"] = quantity;
                Log.Information("Redirected to Order Controller: OrderConfirmation");
                return RedirectToAction(nameof(OrderConfirmation));
            } catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // Get
        public ActionResult OrderConfirmation()
        {
            Log.Information("UI attempt to retrieve order");
            Order order = _orderBL.ViewOrder(Int32.Parse(TempData["OrderID"].ToString()));
            Log.Information("UI attempt to retrieve customer");
            Customer customer = _customerBL.SearchCustomer(order.CustomerID);
            string customerName = customer.FirstName + " " + customer.LastName;
            Log.Information("UI attempt to retrieve location");
            Location location = _locationBL.GetLocationById(order.LocationID);
            ViewData["Total"] = TempData["OrderTotal"];
            ViewData["Customer"] = customerName;
            ViewData["Location"] = location.StoreName;
            ViewData["OrderID"] = TempData["OrderID"];
            string orderTotal = TempData["OrderTotal"].ToString();
            TempData["OrderTotal"] = orderTotal;
            string orderId = TempData["OrderID"].ToString();
            TempData["OrderID"] = orderId;
            OrderVM orderVM = new OrderVM(order);
            return View(orderVM);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderConfirmation(string confirm)
        {
            if (ModelState.IsValid && !String.IsNullOrWhiteSpace(confirm))
            {
                try
                {
                    switch(confirm)
                    {
                        case "Place Order":
                            Log.Information("UI attempt to retrieve order");
                            Order order = _orderBL.ViewOrder(Int32.Parse(TempData["OrderID"].ToString()));
                            order.Total = Double.Parse(TempData["OrderTotal"].ToString());
                            Log.Information("UI attempt to retrieve customer");
                            Customer customer = _customerBL.SearchCustomer(order.CustomerID);
                            Log.Information("UI attempt to retrieve location");
                            Location location = _locationBL.GetLocationById(order.LocationID);
                            Log.Information("UI sent updated order to BL");
                            _orderBL.UpdateOrder(order, location, customer);
                            List<LineItem> orderItems = _lineItemBL.GetLineItems(Int32.Parse(TempData["OrderID"].ToString()));
                            List<int> quantity = new List<int>();
                            foreach (LineItem lineItem in orderItems)
                            {
                                quantity.Add(lineItem.Quantity);
                            }
                            _inventoryBL.SubtractInventory(location.StoreName, quantity);
                            return RedirectToAction("Index", "Home");

                        case "Cancel Order":
                            Log.Information("UI attempt to retrieve order");
                            Order cancelOrder = _orderBL.ViewOrder(Int32.Parse(TempData["OrderID"].ToString()));
                            Log.Information("UI request order deletion to BL");
                            _orderBL.DeleteOrder(cancelOrder);
                            Log.Information("Redirected to Home Controller: Index");
                            return RedirectToAction("Index", "Home");
                    }
                } catch
                {
                    Log.Information("UI attempt to retrieve order");
                    Order cancelOrder = _orderBL.ViewOrder(Int32.Parse(TempData["OrderID"].ToString()));
                    Log.Information("UI request order deletion to BL");
                    _orderBL.DeleteOrder(cancelOrder);
                    Log.Information("Redirected to Home Controller: Index");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // Get
        public ActionResult Search()
        {
            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(int orderId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["OrderID"] = orderId;
                    Log.Information("Redirected to Order Controller: ViewOrder");
                    return RedirectToAction(nameof(ViewOrder));
                }
            } catch
            {
                return View();
            }
            return View();
        }

        // Get
        public ActionResult ViewOrder(OrderVM orderVM)
        {
            try
            {
                List<OrderVM> customerOrder = new List<OrderVM>();
                Log.Information("UI attempt to retrieve order");
                Order order = _orderBL.ViewOrder(Int32.Parse(TempData["OrderID"].ToString()));
                Log.Information("UI attempt to retrieve customer");
                Customer customer = _customerBL.SearchCustomer(order.CustomerID);
                Log.Information("UI attempt to retrieve location");
                Location location = _locationBL.GetLocationById(order.LocationID);
                ViewData["Customer"] = customer.FirstName + " " + customer.LastName;
                ViewData["Location"] = location.StoreName;
                customerOrder.Add(new OrderVM(order));
                return View(customerOrder);
            } catch
            {
                Log.Information("Redirected to Order Controller: Search");
                return RedirectToAction(nameof(Search));
            }
        }
    }
}