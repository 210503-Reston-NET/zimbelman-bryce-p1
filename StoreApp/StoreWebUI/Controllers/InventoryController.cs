using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBL;
using StoreModels;
using StoreWebUI.Models;

namespace StoreWebUI.Controllers
{
    public class InventoryController : Controller
    {
        public IInventoryBL _inventoryBL;
        public IProductBL _productBL;
        public ILocationBL _locationBL;
        public InventoryController(IInventoryBL inventoryBL, ILocationBL locationBL, IProductBL productBL)
        {
            _inventoryBL = inventoryBL;
            _productBL = productBL;
            _locationBL = locationBL;
        }

        private void IDToNameConverter()
        {
            List<Location> locations = _locationBL.GetAllLocations();
            List<Product> products = _productBL.GetAllProducts();
            foreach (Location location in locations)
            {
                string propName = "location" + location.LocationID.ToString();
                ViewData.Add(propName, location.StoreName);
            }
            foreach (Product item in products)
            {
                string propName = "product" + item.ProductID.ToString();
                ViewData.Add(propName, item.ItemName);
            }
        }

        // GET: Inventory
        public ActionResult Index()
        {
            IDToNameConverter();

            return View(_inventoryBL.GetAllInventories().Select(inventory => new InventoryVM(inventory)).ToList());
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            List<Location> locations = _locationBL.GetAllLocations();
            List<Product> products = _productBL.GetAllProducts();
            List<string> storeNames = new List<string>();
            List<string> itemNames = new List<string>();
            foreach (Location location in locations)
            {
                storeNames.Add(location.StoreName);
            }
            ViewData.Add("locations", storeNames);
            foreach (Product item in products)
            {
                itemNames.Add(item.ItemName);
            }
            ViewData.Add("products", itemNames);

            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryVM inventoryVM)
        {
            try
            {
                List<Location> locations = _locationBL.GetAllLocations();
                List<Product> products = _productBL.GetAllProducts();
                List<string> storeNames = new List<string>();
                List<string> itemNames = new List<string>();
                foreach (Location location in locations)
                {
                    storeNames.Add(location.StoreName);
                }
                ViewData.Add("locations", storeNames);
                foreach (Product item in products)
                {
                    itemNames.Add(item.ItemName);
                }
                ViewData.Add("products", itemNames);
                if (ModelState.IsValid)
                {
                    _inventoryBL.AddInventory(new Inventory
                    {
                        LocationID = inventoryVM.LocationID,
                        ProductID = inventoryVM.ProductID,
                        Quantity = inventoryVM.Quantity
                    }
                       );
                    return RedirectToAction(nameof(Index));
                }
                return View(inventoryVM);
            }
            catch
            {
                return View(inventoryVM);
            }
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int id)
        {
            IDToNameConverter();
            try
            {
                List<InventoryVM> storeInventory = _inventoryBL.GetStoreInventoryByLocation(Int32.Parse(TempData["locationID"].ToString())).Select(inven => new InventoryVM(inven)).ToList();

                return View(storeInventory);
            } catch
            {
                return View();
            }
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<InventoryVM> inventoryVM, int id, IFormCollection collection)
        {
            int i = 0;
            try
            {
                List<Inventory> editInventory = _inventoryBL.GetStoreInventoryByLocation(Int32.Parse(TempData["locationID"].ToString()));
                foreach (InventoryVM inventory in inventoryVM)
                {
                    editInventory[i].Quantity = inventory.Quantity;
                    _inventoryBL.EditInventory(editInventory[i]);
                    i++;
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventory/Delete/5
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