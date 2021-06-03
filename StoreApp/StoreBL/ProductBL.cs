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
        private readonly IRepository _repo;
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

        public Product DeleteProduct(Product product)
        {
            Product toBeDeleted = _repo.GetProduct(product);
            if (toBeDeleted != null)
            {
                Log.Information("BL sent product to DL for deletion");
                return _repo.DeleteProduct(toBeDeleted);
            }
            else
            {
                throw new Exception("Product Does Not Exist");
            }
        }

        public Product EditProduct(Product product)
        {
            Log.Information("BL sent updated product to DL");
            return _repo.EditProduct(product);
        }

        public List<Product> GetAllProducts()
        {
            Log.Information("BL attempt to retrieve list of all products from DL");
            return _repo.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            List<Product> products = GetAllProducts();
            if (products.Count == 0)
            {
                Log.Information("No Products Found");
                throw new Exception("No Products Found");
            }
            else
            {
                foreach (Product item in products)
                {
                    if (id.Equals(item.ProductID))
                    {
                        Log.Information("BL sent Product to UI");
                        return item;
                    }
                }
                Log.Information("No matching products found");
                throw new Exception("No matching products found");
            }
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