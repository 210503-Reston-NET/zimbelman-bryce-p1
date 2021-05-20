using System.Collections.Generic;
using StoreModels;

namespace StoreBL
{
    public interface IProductBL
    {
        /// <summary>
        /// Business Logic to add a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product AddProduct(Product product);
        /// <summary>
        /// Business Logic to retrieve a list of all products
        /// </summary>
        /// <returns></returns>
        List<Product> GetAllProducts();
        /// <summary>
        /// Business Logic to calculate the total amount of an order
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        double GetTotal(List<int> quantity);
    }
}