using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBL;
using StoreWebUI.Models;
using StoreModels;
using Serilog;

namespace StoreWebUI.Controllers
{
    public class LocationController : Controller
    {
        private ILocationBL _locationBL;
        private IInventoryBL _inventoryBL;
        private IProductBL _productBL;
        private IOrderBL _orderBL;
        private ICustomerBL _customerBL;

        public LocationController(ILocationBL locationBL, IInventoryBL inventoryBL, IProductBL productBL, IOrderBL orderBL, ICustomerBL customerBL)
        {
            _locationBL = locationBL;
            _inventoryBL = inventoryBL;
            _productBL = productBL;
            _orderBL = orderBL;
            _customerBL = customerBL;
        }

        // GET: Location
        public ActionResult Index()
        {
            Log.Information("UI attempt to retrieve list of locations");
            return View(_locationBL.GetAllLocations().Select(location => new LocationVM(location)).ToList());
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationVM locationVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("UI sent new location to BL");
                    _locationBL.AddLocation(new Location
                    {
                        StoreName = locationVM.StoreName,
                        City = locationVM.City,
                        State = locationVM.State,
                        Address = locationVM.Address
                    });
                    Log.Information("UI attempt to retrieve list of products");
                    List<Product> products = _productBL.GetAllProducts();
                    Log.Information("UI attempt to retrieve location");
                    Location location = _locationBL.GetLocation(locationVM.StoreName);
                    foreach (Product item in products)
                    {
                        Log.Information("UI sent new inventory to BL");
                        _inventoryBL.AddInventory(new Inventory
                        {
                            LocationID = location.LocationID,
                            ProductID = item.ProductID,
                            Quantity = 0
                        });
                    }

                    
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            Log.Information("UI attempt to retrieve location");
            return View(new LocationVM(_locationBL.GetLocationById(id)));
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocationVM locationVM, int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("UI attempt to retrieve location");
                    Location editLocation = _locationBL.GetLocationById(id);
                    editLocation.StoreName = locationVM.StoreName;
                    editLocation.City = locationVM.City;
                    editLocation.State = locationVM.State;
                    editLocation.Address = locationVM.Address;
                    Log.Information("UI sent edited location to BL");
                    _locationBL.EditLocation(editLocation);
                    Log.Information("Redirected to Location Controller: Index");
                    return RedirectToAction(nameof(Index));
                }
                return View();
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            Log.Information("UI attempt to retrieve location");
            return View(new LocationVM(_locationBL.GetLocationById(id)));
        }

        // POST: Location/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Log.Information("UI attempt to retrieve location");
                _locationBL.DeleteLocation(_locationBL.GetLocationById(id));
                Log.Information("Redirected to Location Controller: Index");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Get: View Inventory
        public ActionResult Inventory(int id)
        {
            TempData["locationID"] = id;
            Log.Information("Redirected to Inventory Controller: Edit");
            return RedirectToAction("Edit", "Inventory");
        }

        // Get
        public ActionResult ViewOrders(int id)
        {
            try
            {
                Log.Information("UI attempt to retrieve list of location orders");
                List<OrderVM> orders = _orderBL.GetLocationOrders(id).Select(ord => new OrderVM(ord)).ToList();
                SetListSelectors(id);
                return View(orders);
            } catch
            {
                Log.Information("Redirected to Location Controller: Index");
                return RedirectToAction(nameof(Index));
            }
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewOrders(int id, string sort)
        {
            if (ModelState.IsValid && !String.IsNullOrWhiteSpace(sort))
            {
                try
                {
                    List<OrderVM> sortedOrders = new List<OrderVM>();
                    Log.Information("UI attempt to retrieve list of location orders");
                    List<OrderVM> orders = _orderBL.GetLocationOrders(id).Select(ord => new OrderVM(ord)).ToList();
                    switch (sort)
                    {
                        case "Sort By Cost":
                            Log.Information("Sort by Cost Seleceted");
                            sortedOrders = orders.OrderBy(ord => ord.Total).ToList();
                            SetListSelectors(id);
                            return View(sortedOrders);

                        case "Sort By Date Ascending":
                            Log.Information("Sort by Date Ascending Selected");
                            sortedOrders = orders.OrderByDescending(ord => ord.OrderDate).ToList();
                            SetListSelectors(id);
                            return View(sortedOrders);

                        case "Sort By Date Descending":
                            Log.Information("Sort by Date Descending Selected");
                            sortedOrders = orders.OrderBy(ord => ord.OrderDate).ToList();
                            SetListSelectors(id);
                            return View(sortedOrders);

                    }
                } catch
                {
                    return View();
                }
            }
            return View();
        }

        private void SetListSelectors(int id)
        {
            int i = 0;
            Log.Information("UI attempt to retrieve list of location orders");
            List<OrderVM> orders = _orderBL.GetLocationOrders(id).Select(ord => new OrderVM(ord)).ToList();
            foreach (OrderVM order in orders)
            {
                string customerSelector = "customer" + i;
                Log.Information("UI attempt to retrieve customer");
                Customer customer = _customerBL.SearchCustomer(order.CustomerID);
                string customerName = customer.FirstName + " " + customer.LastName;
                ViewData.Add(customerSelector, customerName);

                string storeSelector = "location" + i;
                Log.Information("UI attempt to retrieve location");
                Location location = _locationBL.GetLocationById(order.LocationID);
                ViewData.Add(storeSelector, location.StoreName);

                i++;
            }
        }
    }
}