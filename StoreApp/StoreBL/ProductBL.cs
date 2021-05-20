using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using Serilog;

namespace StoreBL
{
    /// <summary>
    /// Businss logic class for product model
    /// </summary>
    public class ProductBL : IProductBL
    {
        private IRepository _repo;
        public ProductBL(IRepository repo) {
            _repo = repo;
        }
        public Product AddProduct(Product product)
        {
            if (_repo.GetProduct(product) != null) {
                Log.Information("Product already exists");
                throw new Exception ("Product already exists");
            }
            Log.Information("BL sent product to DL");
            return _repo.AddProduct(product);
        }

        public List<Product> GetAllProducts()
        {
            Log.Information("BL attempt to retrieve list of all products from DL");
            return _repo.GetAllProducts();
        }

        public double GetTotal(List<int> quantity)
        {
            List<Product> products = GetAllProducts();
            int i = 0;
            double total = 0;
            foreach (Product item in products)
            {
                total += quantity[i] * item.Price;
                i++;
            }
            total = Math.Round(total, 2);
            Log.Information("BL calcualted and sent order total to UI");
            return total;
        }
    }
}