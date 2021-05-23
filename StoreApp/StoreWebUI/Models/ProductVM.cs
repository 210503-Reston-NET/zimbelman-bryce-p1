using System;
using StoreModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    public class ProductVM
    {
        public ProductVM(Product product)
        {
            ProductID = product.ProductID;
            ItemName = product.ItemName;
            Price = product.Price;
            Description = product.Description;
        }

        public ProductVM()
        {

        }


        /// <summary>
        /// This represents a unique value for every product
        /// </summary>
        /// <value></value>
        public int ProductID { get; set; }
        /// <summary>
        /// This represents the name of an item
        /// </summary>
        /// <value></value>
        [Required]
        [DisplayName("Product Name")]
        public string ItemName { get; set; }

        /// <summary>
        /// This represents the price of an item
        /// </summary>
        /// <value></value>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// This represents a description of an item
        /// </summary>
        /// <value></value>
        [Required]
        public string Description { get; set; }
    }
}
