using System.Collections.Generic;
using System.Linq;
using Model = StoreModels;
using StoreModels;
using Serilog;

namespace StoreDL
{
    public class RepoDB : IRepository
    {
        private MochaMomentDBContext _context;
        public RepoDB(MochaMomentDBContext context) {
            _context = context;
        }
        public Model.Customer AddCustomer(Model.Customer customer)
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


        public Model.LineItem AddLineItem(Model.LineItem lineItem, Model.Product product)
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

        public Model.Location AddLocation(Model.Location location)
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

        public Model.Order AddOrder(Model.Order order, Model.Location location, Model.Customer customer)
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

        public Model.Product AddProduct(Model.Product product)
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

        public List<Model.Customer> GetAllCustomers()
        {
            Log.Information("DL sent list of all customers to BL");
            return _context.Customers.Select(customer =>
                new Model.Customer(customer.CustomerID, customer.FirstName, customer.LastName, customer.Birthdate, customer.PhoneNumber, customer.Email, customer.MailAddress)
            ).ToList();
        }

        public List<Model.Inventory> GetAllInventories()
        {
            Log.Information("DL sent list of all inventories to BL");
            return _context.Inventories.Select(inventory =>
            new Model.Inventory(inventory.InventoryID, inventory.LocationID, inventory.ProductID, inventory.Quantity)
            ).ToList();
        }

        public List<Model.LineItem> GetAllLineItems()
        {
            Log.Information("DL sent list of all line items to BL");
            return _context.LineItems.Select(lineItem => 
            new Model.LineItem(lineItem.ProductID, lineItem.Quantity, lineItem.OrderID)
            ).ToList();
        }

        public List<Model.Location> GetAllLocations()
        {
            Log.Information("DL sent list of all locations to BL");
            return _context.Locations.Select(location =>
            new Model.Location(location.LocationID, location.StoreName, location.Address, location.City, location.State)
            ).ToList();
        }

        public List<Model.Order> GetAllOrders()
        {
            Log.Information("DL sent list of all orders to BL");
            return _context.Orders.Select(order =>
            new Model.Order(order.LocationID, order.CustomerID, order.OrderID, order.Total, order.OrderDate)
            ).ToList();
        }

        public List<Model.Product> GetAllProducts()
        {
            Log.Information("DL sent list of all products to BL");
            return _context.Products.Select(product =>
            new Model.Product(product.ProductID, product.ItemName, product.Price, product.Description)
            ).ToList();
        }

        public Model.Customer GetCustomer(Model.Customer customer)
        {
            Customer found = _context.Customers.FirstOrDefault(custo => custo.FirstName == customer.FirstName && custo.LastName == customer.LastName && custo.Birthdate == customer.Birthdate && custo.PhoneNumber == customer.PhoneNumber && custo.Email == customer.Email && custo.MailAddress == customer.MailAddress);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent customer to BL");
            return new Model.Customer(found.CustomerID, found.FirstName, found.LastName, found.Birthdate, found.PhoneNumber, found.Email, found.MailAddress);
        }

        public Model.LineItem GetLineItem(Model.LineItem lineItem)
        {
            LineItem found = _context.LineItems.FirstOrDefault( li => li.ProductID == lineItem.ProductID && li.Quantity == lineItem.Quantity && li.OrderID == lineItem.OrderID);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent line item to BL");
            return new Model.LineItem(found.LineItemID, lineItem.ProductID, found.Quantity, found.OrderID);
        }

        public Model.Location GetLocation(Model.Location location)
        {
            Location found = _context.Locations.FirstOrDefault(loca => loca.StoreName == location.StoreName && loca.Address == location.Address && loca.City == location.City && loca.State == location.State);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent location to BL");
            return new Model.Location(found.LocationID, found.StoreName, found.Address, found.City, found.State);
        }

        public Order GetOrder(Order order)
        {
            Order found = _context.Orders.FirstOrDefault(ord => ord.LocationID == order.LocationID && ord.CustomerID == order.CustomerID && ord.Total == order.Total && ord.OrderDate == order.OrderDate);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent order to BL");
            return new Model.Order(found.OrderID , order.LocationID, order.CustomerID, found.Total, found.OrderDate);
        }

        public Order DeleteOrder(Order order) {
            Order deleteOrder = _context.Orders.Single(ord => ord.OrderID == order.OrderID);
            if (deleteOrder != null) {
                _context.Orders.Remove(deleteOrder);
                _context.SaveChanges();
                Log.Information("DL deleted order from DB");
                return order;
            }
            return null;
        }

        public Model.Product GetProduct(Model.Product product)
        {
            Product found = _context.Products.FirstOrDefault(prod => prod.ItemName == product.ItemName && prod.Price == product.Price && prod.Description == product.Description);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent product to BL");
            return new Model.Product(found.ProductID, found.ItemName, found.Price, found.Description);
        }

        public Inventory GetStoreInventory(Model.Inventory inventory)
        {
            Inventory found = _context.Inventories.FirstOrDefault(inven => inven.LocationID == inventory.LocationID && inven.ProductID == inventory.ProductID && inven.Quantity == inventory.Quantity);
            if (found == null) {
                return null;
            }
            Log.Information("DL sent store inventory to BL");
            return new Model.Inventory(found.InventoryID, inventory.LocationID, inventory.ProductID, found.Quantity);
        }

        public Model.Inventory AddInventory(Model.Inventory inventory, Model.Location location, Model.Product product)
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

        public Inventory UpdateInventory(Model.Inventory inventory, Model.Location location, Model.Product product)
        {
            Inventory updateInventory = _context.Inventories.Single(inven => inven.InventoryID == inventory.InventoryID);
            updateInventory.Quantity = inventory.Quantity;
            _context.SaveChanges();
            Log.Information("DL persisted inventory update to DB");
            return inventory;
        }

        public Model.Order UpdateOrder(Model.Order order, Model.Location location, Model.Customer customer) {
            Order updateOrder = _context.Orders.Single(ord => ord.OrderID == order.OrderID);
            updateOrder.Total = order.Total;
            _context.SaveChanges();
            Log.Information("DL persisted order update to DB");
            return order;
        }

        public LineItem DeleteLineItem(LineItem lineItem)
        {
            var deleteLineItem = _context.LineItems.Single(li => li.LineItemID == lineItem.LineItemID);
            if (deleteLineItem != null) {
                _context.LineItems.Remove(deleteLineItem);
                _context.SaveChanges();
                Log.Information("DL deleted line item from DB");
                return lineItem;
            }
            return null;
        }
    }
}