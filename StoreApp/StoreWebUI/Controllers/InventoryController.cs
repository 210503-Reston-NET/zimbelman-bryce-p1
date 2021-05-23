using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBL;
using StoreModels;
using StoreWebUI.Models;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreDL;

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
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryVM inventoryVM)
        {
            try
            {
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
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int id)
        {
            IDToNameConverter();
            return View(new InventoryVM(_inventoryBL.GetStoreInventory(id)));
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InventoryVM inventoryVM, int id, IFormCollection collection)
        {
            try
            {
                Inventory editInventory = _inventoryBL.GetStoreInventory(id);
                editInventory.Quantity = inventoryVM.Quantity;
                _inventoryBL.EditInventory(editInventory);
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