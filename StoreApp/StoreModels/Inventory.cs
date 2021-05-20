using System;
namespace StoreModels
{
    public class Inventory
    {
        /// <summary>
        /// Inventory Model
        /// </summary>
        /// <param name="location"></param>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public Inventory(int locationId, int productId, int quantity) {
            this.LocationID = locationId;
            this.ProductID = productId;
            this.Quantity = quantity;
        }

        public Inventory() {
            
        }

        public Inventory(int inventoryId,int locationId, int productId, int quantity) : this(locationId, productId, quantity) {
            this.InventoryID = inventoryId;
        }

        [Serializable]
        public class NotEnoughInventoryException : Exception {
            public NotEnoughInventoryException(string message) : base(message) {}
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
        public int LocationID { get; set; }
        /// <summary>
        /// This represents a unique value for every product
        /// </summary>
        /// <value></value>
        public int ProductID { get; set; }
        /// <summary>
        /// This represents a stores quantity of a product
        /// </summary>
        /// <value></value>
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{Quantity}";
        }
    }
}