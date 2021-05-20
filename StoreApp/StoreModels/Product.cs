namespace StoreModels
{

    /// <summary>
    /// Product Model
    /// </summary>
    public class Product
    {
            public Product(string itemName, double price, string description) {
                this.ItemName = itemName;
                this.Price = price;
                this.Description = description;
        }
        
        public Product() {
            
        }

        public Product(int id, string itemName, double price, string description) : this(itemName, price, description) {
            this.Id = id;
        }

        /// <summary>
        /// This represents a unique value for every product
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// This represents the name of an item
        /// </summary>
        /// <value></value>
        public string ItemName { get; set; }

        /// <summary>
        /// This represents the price of an item
        /// </summary>
        /// <value></value>
        public double Price { get; set; }

        /// <summary>
        /// This represents a description of an item
        /// </summary>
        /// <value></value>
        public string Description { get; set; }
        public int ProductID { get; set; }

        public override string ToString()
        {
            return $"Item: {ItemName} \nPrice: ${Price} \nDescription: {Description}\n";
        }

        public bool Equals(Product product) {
            return this.ItemName.Equals(product.ItemName);
        }
    }
}