using System.Collections.Generic;
using System.Linq;
using StoreModels;
using Serilog;

namespace StoreDL
{
    public class RepoDB : IRepository
    {
        private readonly MochaMomentDBContext _context;
        public RepoDB(MochaMomentDBContext context) {
            _context = context;
        }
        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(
                new Customer {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Birthdate = customer.Birthdate,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                MailAddress = customer.MailAddress
                }
            );
            _context.SaveChanges();
            Log.Information("DL persisted customer add to DB");
            return customer;
        }


        public LineItem AddLineItem(LineItem lineItem, Product product)
        {
            _context.LineItems.Add(
                new LineItem {
                    ProductID = lineItem.ProductID,
                    Quantity = lineItem.Quantity,
                    OrderID =lineItem.OrderID
                }
            );
            Log.Information("DL persisted line item add to DB");
            _context.SaveChanges();
            return lineItem;
        }

        public Location AddLocation(Location location)
        {
            _context.Locations.Add(
                new Location {
                    StoreName = location.StoreName,
                    Address = location.Address,
                    City = location.City,
                    State = location.State
                }
            );
            Log.Information("DL persisted location add to DB");
            _context.SaveChanges();
            return location;
        }

        public Order AddOrder(Order order, Location location, Customer customer)
        {
            _context.Orders.Add(
                new Order {
                    LocationID = order.LocationID,
                    CustomerID = order.CustomerID,
                    Total = order.Total,
                    OrderDate = order.OrderDate
                }
            );
            Log.Information("DL persisted order add to DB");
            _context.SaveChanges();
            return order;
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(
                new Product {
                    ItemName = product.ItemName,
                    Price = product.Price,
                    Description = product.Description
                }
            );
            Log.Information("DL persisted product add to DB");
            _context.SaveChanges();
            return product;
        }

        public List<Customer> GetAllCustomers()
        {
            Log.Information("DL sent list of all customers to BL");
            return _context.Customers.Select(customer =>
                new Customer(customer.CustomerID, customer.FirstName, customer.LastName, customer.Birthdate, customer.PhoneNumber, customer.Email, customer.MailAddress)
            ).ToList();
        }

        public List<Inventory> GetAllInventories()
        {
            Log.Information("DL sent list of all inventories to BL");
            return _context.Inventories.Select(inventory =>
            new Inventory(inventory.InventoryID, inventory.LocationID, inventory.ProductID, inventory.Quantity)
            ).ToList();
        }

        public List<LineItem> GetAllLineItems()
        {
            Log.Information("DL sent list of all line items to BL");
            return _context.LineItems.Select(lineItem => 
            new LineItem(lineItem.ProductID, lineItem.Quantity, lineItem.OrderID)
            ).ToList();
        }

        public List<Location> GetAllLocations()
        {
            Log.Information("DL sent list of all locations to BL");
            return _context.Locations.Select(location =>
            new Location(location.LocationID, location.StoreName, location.Address, location.City, location.State)
            ).ToList();
        }

        public List<Order> GetAllOrders()
        {
            Log.Information("DL sent list of all orders to BL");
            return _context.Orders.Select(order =>
            new Order(order.OrderID, order.CustomerID, order.LocationID, order.Total, order.OrderDate)
            ).ToList();
        }

        public List<Product> GetAllProducts()
        {
            Log.Information("DL sent list of all products to BL");
            return _context.Products.Select(product =>
            new Product(product.ProductID, product.ItemName, product.Price, product.Description)
            ).ToList();
        }

        public Customer GetCustomer(Customer customer)
        {
            Customer found = _context.Customers.FirstOrDefault(custo => custo.FirstName == customer.FirstName && custo.LastName == customer.LastName && custo.Birthdate == customer.Birthdate && custo.PhoneNumber == customer.PhoneNumber && custo.Email == customer.Email && custo.MailAddress == customer.MailAddress);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent customer to BL");
            return new Customer(found.CustomerID, found.FirstName, found.LastName, found.Birthdate, found.PhoneNumber, found.Email, found.MailAddress);
        }

        public LineItem GetLineItem(LineItem lineItem)
        {
            LineItem found = _context.LineItems.FirstOrDefault( li => li.ProductID == lineItem.ProductID && li.Quantity == lineItem.Quantity && li.OrderID == lineItem.OrderID);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent line item to BL");
            return new LineItem(found.LineItemID, lineItem.ProductID, found.Quantity, found.OrderID);
        }

        public Location GetLocation(Location location)
        {
            Location found = _context.Locations.FirstOrDefault(loca => loca.StoreName == location.StoreName && loca.Address == location.Address && loca.City == location.City && loca.State == location.State);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent location to BL");
            return new Location(found.LocationID, found.StoreName, found.Address, found.City, found.State);
        }

        public Order GetOrder(Order order)
        {
            Order found = _context.Orders.FirstOrDefault(ord => ord.LocationID == order.LocationID && ord.CustomerID == order.CustomerID && ord.Total == order.Total && ord.OrderDate == order.OrderDate);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent order to BL");
            return new Order(found.OrderID , order.LocationID, order.CustomerID, found.Total, found.OrderDate);
        }

        public Order DeleteOrder(Order order) {
            Order deleteOrder = _context.Orders.First(ord => ord.OrderID == order.OrderID);
            if (deleteOrder != null) {
                _context.Orders.Remove(deleteOrder);
                _context.SaveChanges();
                Log.Information("DL deleted order from DB");
                return order;
            }
            return null;
        }

        public Product GetProduct(Product product)
        {
            Product found = _context.Products.FirstOrDefault(prod => prod.ItemName == product.ItemName && prod.Price == product.Price && prod.Description == product.Description);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent product to BL");
            return new Product(found.ProductID, found.ItemName, found.Price, found.Description);
        }

        public Inventory GetStoreInventory(Inventory inventory)
        {
            Inventory found = _context.Inventories.FirstOrDefault(inven => inven.LocationID == inventory.LocationID && inven.ProductID == inventory.ProductID && inven.Quantity == inventory.Quantity);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent store inventory to BL");
            return new Inventory(found.InventoryID, inventory.LocationID, inventory.ProductID, found.Quantity);
        }

        public Inventory GetStoreInventory(int inventoryId)
        {
            return _context.Inventories.Find(inventoryId);
        }

        public Inventory AddInventory(Inventory inventory, Location location, Product product)
        {
            _context.Inventories.Add(
                new Inventory {
                    InventoryID = inventory.InventoryID,
                    LocationID = GetLocation(location).LocationID,
                    ProductID = GetProduct(product).ProductID,
                    Quantity = inventory.Quantity
                    }
                );
                _context.SaveChanges();
                Log.Information("DL persisted inventory add to DB");
                return inventory;
        }

        public Inventory UpdateInventory(Inventory inventory)
        {
            Inventory updateInventory = _context.Inventories.First(inven => inven.InventoryID == inventory.InventoryID);
            updateInventory.Quantity = inventory.Quantity;
            _context.SaveChanges();
            Log.Information("DL persisted inventory update to DB");
            return inventory;
        }

        public Order UpdateOrder(Order order, Location location, Customer customer) {
            Order updateOrder = _context.Orders.First(ord => ord.OrderID == order.OrderID);
            updateOrder.Total = order.Total;
            _context.SaveChanges();
            Log.Information("DL persisted order update to DB");
            return order;
        }

        public LineItem DeleteLineItem(LineItem lineItem)
        {
            var deleteLineItem = _context.LineItems.First(li => li.LineItemID == lineItem.LineItemID);
            if (deleteLineItem != null) {
                _context.LineItems.Remove(deleteLineItem);
                _context.SaveChanges();
                Log.Information("DL deleted line item from DB");
                return lineItem;
            }
            return null;
        }

        public Location GetLocationById(int locationId)
        {
            return _context.Locations.Find(locationId);
        }

        public Location DeleteLocation(Location location)
        {
            Location toBeDeleted = _context.Locations.First(loca => loca.LocationID == location.LocationID);
            _context.Locations.Remove(toBeDeleted);
            _context.SaveChanges();
            return location;
        }

        public Location EditLocation(Location location)
        {
            Location editLocation = _context.Locations.First(loca => loca.LocationID == location.LocationID);
            editLocation.StoreName = location.StoreName;
            editLocation.City = location.City;
            editLocation.State = location.State;
            editLocation.Address = location.Address;
            _context.SaveChanges();
            Log.Information("DL persisted location update to DB");
            return location;
        }

        public Customer EditCustomer(Customer customer)
        {
            Customer editCustomer = _context.Customers.First(custo => custo.CustomerID == customer.CustomerID);
            editCustomer.FirstName = customer.FirstName;
            editCustomer.LastName = customer.LastName;
            editCustomer.Birthdate = customer.Birthdate;
            editCustomer.PhoneNumber = customer.PhoneNumber;
            editCustomer.Email = customer.Email;
            editCustomer.MailAddress = customer.MailAddress;
            _context.SaveChanges();
            Log.Information("DL persisted customer update to DB");
            return customer;
        }

        public Customer DeleteCustomer(Customer customer)
        {
            Customer toBeDeleted = _context.Customers.First(custo => custo.CustomerID == customer.CustomerID);
            _context.Customers.Remove(toBeDeleted);
            _context.SaveChanges();
            Log.Information("DL deleted customer from DB");
            return customer;
        }

        public Product EditProduct(Product product)
        {
            Product editProduct = _context.Products.First(prod => prod.ProductID == product.ProductID);
            editProduct.ItemName = product.ItemName;
            editProduct.Price = product.Price;
            editProduct.Description = product.Description;
            _context.SaveChanges();
            Log.Information("BL persisted prodcut update to DB");
            return product;
        }

        public Product DeleteProduct(Product product)
        {
            Product toBeDeleted = _context.Products.First(prod => prod.ProductID == product.ProductID);
            _context.Products.Remove(toBeDeleted);
            _context.SaveChanges();
            Log.Information("DL deleted product from DB");
            return product;
        }
    }
}