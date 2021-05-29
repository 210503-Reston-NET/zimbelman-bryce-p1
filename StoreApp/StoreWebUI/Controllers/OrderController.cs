using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBL;
using StoreModels;

namespace StoreWebUI.Controllers
{
    public class OrderController : Controller
    {
        public IOrderBL _orderBL;
        public ICustomerBL _customerBL;
        public ILocationBL _locationBL;

        public OrderController(IOrderBL orderBL, ICustomerBL customerBL, ILocationBL locationBL)
        {
            _orderBL = orderBL;
            _customerBL = customerBL;
            _locationBL = locationBL;
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
                    if (_customerBL.SearchCustomer(firstName, lastName) != null)
                    {
                        TempData["firstName"] = firstName;
                        TempData["lastName"] = lastName;
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
                    Customer customer = _customerBL.SearchCustomer(TempData["firstName"].ToString(), TempData["lastName"].ToString());
                    Location location = _locationBL.GetLocation(storeName);
                    int orderID = 0;
                    if (location == null)
                    {
                        return View();
                    }
                    Order newOrder = new Order(location.LocationID, customer.CustomerID, 0, DateTime.Now.ToString());
                    _orderBL.AddOrder(newOrder, location, customer);
                    List<Order> orders = _orderBL.GetAllOrders();
                    // Retrieves latest orderID
                    foreach (Order order in orders)
                    {
                        orderID = order.OrderID;
                    }
                    TempData["OrderID"] = orderID;
                    return RedirectToAction(nameof(LineItems));
                }
                return View();
            } catch
            {
                return View();
            }
            
        }

        // Get
        public ActionResult LineItems()
        {
            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LineItems(List<int> quantity)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}