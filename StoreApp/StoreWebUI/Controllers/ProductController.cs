using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreModels;
using StoreBL;
using StoreWebUI.Models;
using Serilog;

namespace StoreWebUI.Controllers
{
    public class ProductController : Controller
    {
        public IProductBL _productBL;
        public ProductController(IProductBL productBL)
        {
            _productBL = productBL;
        }

        // GET: Product
        public ActionResult Index()
        {
            Log.Information("UI attempt to retrieve product");
            return View(_productBL.GetAllProducts().Select(product => new ProductVM(product)).ToList());
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM productVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("UI sent new product to BL");
                    _productBL.AddProduct(new Product {
                        ItemName = productVM.ItemName,
                        Price = productVM.Price,
                        Description = productVM.Description

                    });
                    Log.Information("Redirected to Product Controller: Index");
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Log.Information("UI attempt to retrieve product");
            return View(new ProductVM(_productBL.GetProductById(id)));
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductVM productVM, int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("UI attempt to retrieve product");
                    Product editProduct = _productBL.GetProductById(id);
                    editProduct.ItemName = productVM.ItemName;
                    editProduct.Price = productVM.Price;
                    editProduct.Description = productVM.Description;
                    Log.Information("UI sent edited product to BL");
                    _productBL.EditProduct(editProduct);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            Log.Information("UI attempt to retrieve product");
            return View(new ProductVM(_productBL.GetProductById(id)));
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Log.Information("UI attempt to retrieve product");
                _productBL.DeleteProduct(_productBL.GetProductById(id));
                Log.Information("Redirected to Product Controller: Index");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}