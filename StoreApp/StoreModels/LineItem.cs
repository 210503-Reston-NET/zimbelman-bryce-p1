using System.Collections.Generic;
namespace StoreModels
{
    /// <summary>
    /// Line Item Model
    /// </summary>
    public class LineItem
    {
        public LineItem(int productId, int quantity, int orderID) {
            this.ProductID = productId;
            this.Quantity = quantity;
            this.OrderID = orderID;
        }

        public LineItem() {
            
        }

        public LineItem(int lineItemId, int productId, int quantity, int orderID) : this(productId, quantity, orderID) {
            this.LineItemID = lineItemId;
        }

        /// <summary>
        /// This represents a unqiue vale for every line item
        /// </summary>
        /// <value></value>
        public int LineItemID { get; set; }
        /// <summary>
        /// This represents a unqiue value for every product
        /// </summary>
        /// <value></value>
        public int ProductID { get; set; }
        /// <summary>
        /// This represents the quantity of an item from a specific order
        /// </summary>
        /// <value></value>
        public int Quantity { get; set; }
        /// <summary>
        /// This represents a unique value for every order
        /// </summary>
        /// <value></value>
        public int OrderID { get; set; }



        public override string ToString()
        {
            return $"{Quantity}";
        }
    }
}