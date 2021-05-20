using System;
using Xunit;
using StoreModels;
namespace StoreTests
{
    public class CustomerTest
    {
        [Fact]
        public void CustomerShouldSetValidData() {
            string firstName = "Kylie";
            Customer test = new Customer("firstName", "lastName","birthdate", "phone#", "email", "Mailing Address");

            test.FirstName = firstName;

            Assert.Equal(firstName, test.FirstName);
        }

        [Fact]
        public void OrderShouldSetValidData() {
            double total = 2.99;
            Order test = new Order(1, 1, 1, 1.99, "");

            test.Total = total;

            Assert.Equal(total, test.Total);
        }

        [Fact]
        public void InventoryShouldSetValidData() {
            int quantity = 1;
            Inventory test = new Inventory(1, 1, 1);

            test.Quantity = quantity;

            Assert.Equal(quantity, test.Quantity);
        }

        [Fact]
        public void LineItemShouldSetValidData() {
            int quantity = 1;
            LineItem test = new LineItem(1, 1, 1);

            test.Quantity = quantity;

            Assert.Equal(quantity, test.Quantity);
        }

        [Fact]
        public void LocationShouldSetValidData() {
            string city = "Portland";
            Location test = new Location("name", "address", "city", "state");

            test.City = city;
            
            Assert.Equal(city, test.City);
        }

        [Fact]
        public void ProductShouldSetValidData() {
            string itemName = "Mocha";
            Product test = new Product("name", 1.99, "description");

            test.ItemName = itemName;

            Assert.Equal(itemName, test.ItemName);
        }

        [Fact]
        public void PriceShouldsetValidData() {
            double price = 2.90;
            Product test = new Product("name", 1.99, "description");
            
            test.Price = price;
            
            Assert.Equal(price, test.Price);
        }
    }
}