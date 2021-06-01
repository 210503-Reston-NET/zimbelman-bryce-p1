using Model = StoreModels;
using Microsoft.EntityFrameworkCore;
using Xunit;
using StoreDL;
using System.Linq;

namespace StoreTests
{
    public class RepoTest
    {
        private readonly DbContextOptions<MochaMomentDBContext> options;
        public RepoTest()
        {
            options = new DbContextOptionsBuilder<MochaMomentDBContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }
        private void Seed() {
            using (var context = new MochaMomentDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Products.AddRange(
                    new Model.Product
                    {
                        ProductID = 1,
                        ItemName = "Mocha",
                        Price = 1.99,
                        Description = "Mocha?"
                    },
                    new Model.Product
                    {
                        ProductID = 2,
                        ItemName = "Frost",
                        Price = 3.49,
                        Description = "Frost?"
                    }
                );

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Customers.AddRange(
                    new Model.Customer
                    {
                        CustomerID = 1,
                        FirstName = "Bryce",
                        LastName = "Zimbelman",
                        Birthdate = "10/15/1994",
                        PhoneNumber = "920-264-4500",
                        Email = "bryce.zimbelman@revature.net",
                        MailAddress = "1514 Canyon Dr"
                    },
                    new Model.Customer
                    {
                        CustomerID = 2,
                        FirstName = "Kylie",
                        LastName = "Zimbelman",
                        Birthdate = "06/06/1998",
                        PhoneNumber = "717-702-8156",
                        Email = "kylie.zimbelman@icloud.com",
                        MailAddress = "1514 Canyon Dr"
                    }
                    );

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Locations.AddRange(
                    new Model.Location
                    {
                        LocationID = 1,
                        StoreName = "JVL1",
                        Address = "1544 Gateway Blvd",
                        City = "Beloit",
                        State = "WI"
                    },
                    new Model.Location
                    {
                        LocationID = 2,
                        StoreName = "St. Mary's",
                        Address = "3200 E Racine St",
                        City = "Janesville",
                        State = "WI"
                    }
                    );

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Inventories.AddRange(
                    new Model.Inventory
                    {
                        InventoryID = 1,
                        LocationID = 1,
                        ProductID = 1,
                        Quantity = 1
                    },
                    new Model.Inventory
                    {
                        InventoryID = 2,
                        LocationID = 1,
                        ProductID = 1,
                        Quantity = 2
                    }
                    );

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Orders.AddRange(
                    new Model.Order
                    {
                        OrderID = 1,
                        CustomerID = 1,
                        LocationID = 1,
                        Total = 3.49,
                        OrderDate = "Today"
                    },
                    new Model.Order
                    {
                        OrderID = 2,
                        CustomerID = 1,
                        LocationID = 1,
                        Total = 1.99,
                        OrderDate = "Yesterday"
                    }
                    );

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.LineItems.AddRange(
                    new Model.LineItem
                    {
                        LineItemID = 1,
                        ProductID = 1,
                        Quantity = 1,
                        OrderID = 1
                    },
                    new Model.LineItem
                    {
                        LineItemID = 2,
                        ProductID = 1,
                        Quantity = 2,
                        OrderID = 2
                    }
                    );
                context.SaveChanges();
            }
        }
        [Fact]
        public void GetAllProductsShouldReturnAllProducts() {
            using (var context = new MochaMomentDBContext(options)) {
                IRepository _repo = new RepoDB(context);
                var products = _repo.GetAllProducts();
                Assert.Equal(2, products.Count);
            }
        }

        [Fact]
        public void GetAllCustomersShouldReturnAllCustomers()
        {
            using (var context = new MochaMomentDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                var customers = _repo.GetAllCustomers();
                Assert.Equal(2, customers.Count);
            }
        }

        [Fact]
        public void GetAllLocationsShouldReturnAllLocations()
        {
            using (var context = new MochaMomentDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                var locations = _repo.GetAllLocations();
                Assert.Equal(2, locations.Count);
            }
        }

        [Fact]
        public void GetAllInventoriesShouldReturnAllInventories()
        {
            using (var context = new MochaMomentDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                var inventories = _repo.GetAllInventories();
                Assert.Equal(2, inventories.Count);
            }
        }

        [Fact]
        public void GetAllOrdersShouldReturnAllOrders()
        {
            using (var context = new MochaMomentDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                var orders = _repo.GetAllOrders();
                Assert.Equal(2, orders.Count);
            }
        }

        [Fact]
        public void GetAllLineItemsShouldReturnAllLineItems()
        {
            using (var context = new MochaMomentDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                var lineItems = _repo.GetAllLineItems();
                Assert.Equal(2, lineItems.Count);
            }
        }

        [Fact]
        public void AddProductShouldAddProduct() {
            using (var context = new MochaMomentDBContext(options)) {
                IRepository _repo = new RepoDB(context);
                _repo.AddProduct(new Model.Product("Espresso", 1.99, "Espresso?"));
            }

            using (var assertContext = new MochaMomentDBContext(options)) {
                var result = assertContext.Products.FirstOrDefault(product => product.ProductID == 3);
                Assert.NotNull(result);
                Assert.Equal("Espresso", result.ItemName);
            }
        }
        [Fact]
        public void AddLineItemShouldAddLineItem() {
            using (var context = new MochaMomentDBContext(options)) {
                IRepository _repo = new RepoDB(context);
                _repo.AddCustomer(new Model.Customer("firstName", "lastName","birthdate", "phone#", "email", "Mailing Address"));
                _repo.AddLocation(new Model.Location("name", "address", "city", "state"));
                _repo.AddProduct(new Model.Product("name", 1.99, "description"));
                _repo.AddOrder(new Model.Order(1, 1, 1, 1.99, ""), new Model.Location("name", "address", "city", "state"), new Model.Customer("firstName", "lastName","birthdate", "phone#", "email", "Mailing Address"));
                _repo.AddLineItem(new Model.LineItem(1, 1, 1), new Model.Product("name", 1.99, "description"));
            }

            using (var assertContext = new MochaMomentDBContext(options)) {
                var result = assertContext.LineItems.FirstOrDefault(li => li.LineItemID == 3);
                Assert.NotNull(result);
                Assert.Equal(1, result.Quantity);
            }
        }

        [Fact]
        public void AddCustomerShouldAddCustomer()
        {
            using (var context = new MochaMomentDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                _repo.AddCustomer(new Model.Customer("firstName", "lastName", "birthdate", "phone#", "email", "Mailing Address"));
            }

            using (var assertContext = new MochaMomentDBContext(options))
            {
                var result = assertContext.Customers.FirstOrDefault(custo => custo.CustomerID == 3);
                Assert.NotNull(result);
                Assert.Equal("firstName", result.FirstName);
            }
        }

        [Fact]
        public void AddInventoryShouldAddInventory()
        {
            using (var context = new MochaMomentDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                _repo.AddLocation(new Model.Location("name", "address", "city", "state"));
                _repo.AddProduct(new Model.Product("name", 1.99, "description"));
                _repo.AddInventory(new Model.Inventory(1, 1, 1), new Model.Location("name", "address", "city", "state"), new Model.Product("name", 1.99, "description"));
            }

            using (var assertContext = new MochaMomentDBContext(options))
            {
                var result = assertContext.Inventories.FirstOrDefault(inven => inven.InventoryID == 3);
                Assert.NotNull(result);
                Assert.Equal(1, result.Quantity);
            }
        }

        [Fact]
        public void AddLocationShouldAddLocation()
        {
            using (var context = new MochaMomentDBContext(options)) {
                IRepository _repo = new RepoDB(context);
                _repo.AddLocation(new Model.Location("name", "address", "city", "state"));
            }

            using (var assertContext = new MochaMomentDBContext(options))
            {
                var result = assertContext.Locations.FirstOrDefault(loca => loca.LocationID == 3);
                Assert.NotNull(result);
                Assert.Equal("name", result.StoreName);
            }
        }

        [Fact]
        public void AddOrderShouldAddOrder()
        {
            using (var context = new MochaMomentDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                _repo.AddLocation(new Model.Location("name", "address", "city", "state"));
                _repo.AddCustomer(new Model.Customer("firstName", "lastName", "birthdate", "phone#", "email", "Mailing Address"));
                _repo.AddOrder(new Model.Order(1, 1, 1.99, "orderDate"), new Model.Location("name", "address", "city", "state"), new Model.Customer("firstName", "lastName", "birthdate", "phone#", "email", "Mailing Address"));
            }

            using (var assertContext = new MochaMomentDBContext(options))
            {
                var result = assertContext.Orders.FirstOrDefault(ord => ord.OrderID == 3);
                Assert.NotNull(result);
                Assert.Equal(1.99, result.Total);
            }
        }
    }
}
