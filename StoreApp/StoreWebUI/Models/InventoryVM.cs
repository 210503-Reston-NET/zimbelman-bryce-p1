using System;
using StoreModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace StoreWebUI.Models
{
    public class InventoryVM
    {
        public InventoryVM()
        {

        }

        public InventoryVM(Inventory inventory)
        {
            InventoryID = inventory.InventoryID;
            LocationID = inventory.LocationID;
            ProductID = inventory.ProductID;
            Quantity = inventory.Quantity;
        }

        /// <summary>
        /// This represents a unique value for every inventory
        /// </summary>
        /// <value></value>
        public int InventoryID { get; set; }
        /// <summary>
        /// This represents a unique value for every location
        /// </summary>
        /// <value></value>
        [DisplayName("Store Location")] 
        public int LocationID { get; set; }
        /// <summary>
        /// This represents a unique value for every product
        /// </summary>
        /// <value></value>
        [DisplayName("Product")]
        public int ProductID { get; set; }
        /// <summary>
        /// This represents a stores quantity of a product
        /// </summary>
        /// <value></value>
        [Required]
        public int Quantity { get; set; }
    }
}
