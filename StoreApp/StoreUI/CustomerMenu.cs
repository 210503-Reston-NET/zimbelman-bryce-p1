using System;
using StoreModels;
using StoreBL;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace StoreUI
{
    public class CustomerMenu : IMenu
    {
        private ICustomerBL _customerBL;
        private IProductBL _productBL;
        private ILocationBL _locationBL;
        private IOrderBL _orderBL;
        private ILineItemBL _lineItemBL;
        private IInventoryBL _inventoryBL;

        private IValidationService _validate;

        public CustomerMenu(ICustomerBL customerBL, IProductBL productBL, IOrderBL orderBL, ILocationBL locationBL, ILineItemBL lineItemBL, IInventoryBL inventoryBL, IValidationService validate) {
            _customerBL = customerBL;
            _productBL = productBL;
            _orderBL = orderBL;
            _locationBL = locationBL;
            _lineItemBL = lineItemBL;
            _inventoryBL = inventoryBL;
            _validate = validate;
        }

        public void Start() {
            bool repeat = true;
            do {
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Place an order");
            Console.WriteLine("[2] Add a new customer");
            Console.WriteLine("[3] Display placed order details");
            Console.WriteLine("[4] View order history");
            Console.WriteLine("[0] Go Back");

            // Receives input from user
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                        // Exit
                        Log.Information("Go Back to Previous Menu");
                        repeat = false;
                        break;
                    
                    case "1":
                        Log.Information("Place Order Selected");
                        PlaceOrder();
                        break;

                    case "2":
                        Log.Information("Add Customer Selected");
                        AddACustomer();
                        break;
                    
                    case "3":
                        Log.Information("Search for specific order selected");
                        OrderSearch();
                        break;
                    
                    case "4":
                        Log.Information("View Cusotmer Order History Selected");
                        DisplayOrderHistory();
                        break;

                    default:
                        // Invalid Input
                        Console.WriteLine("Please input a valid option");
                        break;
                }
            } while (repeat);
        }

        /// <summary>
        /// UI to add a customer
        /// </summary>
        private void AddACustomer() {
            Console.WriteLine("\nEnter the details of the customer you want to add");
            string firstName = _validate.ValidateString("Enter the customer first name: ");
            string lastName = _validate.ValidateString("Enter the customer last name");
            string birthdate = _validate.ValidateString("Enter the customer birthdate (MM/DD/YYYY): ");
            string phoneNumber = _validate.ValidateString("Enter the customer phone number: ");
            string email = _validate.ValidateString("Enter the customer email: ");
            string mailAddress = _validate.ValidateString("Enter the customer mailing address: ");
            Log.Information("Customer information input");
            try
            {
                // New customer model created and sent to Business Logic
                Customer newCustomer = new Customer(firstName, lastName,  birthdate, phoneNumber, email, mailAddress);
                Log.Information("UI sent customer to BL");
                Customer createdCustomer = _customerBL.AddCustomer(newCustomer);
                Console.WriteLine("New Customer Created!\n");
                Console.WriteLine(createdCustomer.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// UI to display customer order history
        /// </summary>
        private void DisplayOrderHistory() {
            bool repeat = true;
            string firstName = _validate.ValidateString("\nEnter your first name: ");
            string lastName = _validate.ValidateString("Enter your last name: ");
            Log.Information("Customer information input");
            try {
                // Search for specific customer and retrive orders
                Customer customer = _customerBL.SearchCustomer(firstName, lastName);
                List<Order> orders = _orderBL.GetCustomerOrders(customer.Id);
                List<Order> sortedOrders = new List<Order>();
                do
                {
                    Console.WriteLine("How should the orders be sorted?");
                    Console.WriteLine("[1] Sort by Date (Newest to Oldest)");
                    Console.WriteLine("[2] Sort by Date (Oldest to Newest)");
                    Console.WriteLine("[3] Sort by Cost (Lowest to Highest)");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            repeat = false;
                            Log.Information("Sort by Date Ascending Selected");
                            sortedOrders = orders.OrderByDescending(ord => ord.OrderDate).ToList();
                            break;

                        case "2":
                            repeat = false;
                            Log.Information("Sort by Date Descending Selected");
                            sortedOrders = orders.OrderBy(ord => ord.OrderDate).ToList();
                            break;

                        case "3":
                            repeat = false;
                            Log.Information("Sort by Cost Ascending Seleceted");
                            sortedOrders = orders.OrderBy(ord => ord.Total).ToList();
                            break;
                        
                        default:
                            // Invalid Input
                            Console.WriteLine("Please input a valid option");
                            break;
                    }
                } while (repeat);
                // Iterate through, orders, line items, and products to display order information
                foreach (Order order in sortedOrders)
                {
                    List<LineItem> lineItems = _lineItemBL.GetLineItems(order.OrderID);
                    Location location = _locationBL.GetLocation(order.LocationID);
                    Console.WriteLine($"\nCustomer Name: {customer.FirstName} {customer.LastName} \nLocation Name: {location.StoreName} \nOrder Date: {order.OrderDate}");
                    foreach (LineItem lineItem in lineItems)
                    {
                        List<Product> products = _productBL.GetAllProducts();
                        foreach (Product product in products)
                        {
                            if (product.Id.Equals(lineItem.ProductID)) {
                                Console.WriteLine($"{lineItem.Quantity} {product.ItemName}");
                            }
                        }
                    }
                    Console.WriteLine($"Order Total ${order.Total}\n");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            } 
        }

        /// <summary>
        /// UI to search for specific order by orderID
        /// </summary>
        private void OrderSearch() {
            int orderId = _validate.ValidateInt("\nEnter Order ID: ");
            Log.Information("Order ID Input");
            try
            {
                // Search for specific order
                Order customerOrder = _orderBL.ViewOrder(orderId);
                Location location = _locationBL.GetLocation(customerOrder.LocationID);
                Customer customer = _customerBL.SearchCustomer(customerOrder.CustomerID);
                List<LineItem> lineItems = _lineItemBL.GetLineItems(orderId);
                Console.WriteLine($"\nCustomer Name: {customer.FirstName} {customer.LastName} \nLocation Name: {location.StoreName} \nOrder Date: {customerOrder.OrderDate}");
                // Iterate through line items to show purchases
                foreach (LineItem lineItem in lineItems)
                {
                    List<Product> products = _productBL.GetAllProducts();
                    foreach (Product product in products)
                        {
                            if (product.Id.Equals(lineItem.ProductID)) {
                                Console.WriteLine($"{lineItem.Quantity} {product.ItemName}");
                            }
                        }
                }
                Console.WriteLine($"Order Total ${customerOrder.Total}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        /// <summary>
        /// UI to place customer order
        /// </summary>
        private void PlaceOrder() {
            bool orderRepeat = true;
            int i = 0;
            bool orderPlaced = false;
            DateTime orderDate = new DateTime();
            Order deleteOrder = new Order(1, 1, 1, 1.99, "");
            try {
                List<Product> products = _productBL.GetAllProducts();
                List<int> quantity = new List<int>();
                string locationName = _validate.ValidateString("\nEnter name of location to shop at: ");
                Log.Information("Location Selected");
                int orderID = 0;
                Location location = _locationBL.GetLocation(locationName);
                string firstName = _validate.ValidateString("Enter your first name: ");
                string lastName = _validate.ValidateString("Enter your last name: ");
                Log.Information("Customer Name Entered");
                Customer customer = _customerBL.SearchCustomer(firstName, lastName);
                Console.WriteLine("Enter the amount desired of each item");
                // Gets local date for order date and time
                orderDate = DateTime.Now;
                // Create initial new order to generate orderID, used by line items
                Order newOrder = new Order(location.Id, customer.Id, orderID, 0, orderDate.ToString());
                // Add customer information to order
                _orderBL.AddOrder(newOrder, location, customer);
                orderPlaced = true;
                // Used to delete order later if canceled
                deleteOrder = newOrder;
                // Iterate through products to display choices to customer
                foreach (Product item in products)
                {
                    List<Order> orders = _orderBL.GetAllOrders();
                    // Retrieves latest orderID
                    foreach (Order order in orders)
                    {
                        orderID = order.OrderID;
                    }
                    deleteOrder.OrderID = orderID;
                    quantity.Add(_validate.ValidateInt($"{item.ItemName}: "));
                    // Create line item for each product
                    LineItem lineItem = new LineItem(item.Id, quantity[i], orderID);
                    Log.Information("UI sent line item to BL");
                    _lineItemBL.AddLineItem(lineItem, item);
                    i++;
                }
                double total = _productBL.GetTotal(quantity);
                do
                {
                    Console.WriteLine($"\nThe total amount of your order will be ${total} \nWould you like to proceed? (Y/N)");
                    string orderInput = Console.ReadLine();
                    switch (orderInput)
                    {
                        case "Y":
                            orderRepeat = false;
                            Log.Information("Customer to proceed with purchase");
                            newOrder.Total = total;
                            newOrder.OrderID = orderID;
                            Log.Information("UI sent updated inventory to BL");
                            _inventoryBL.SubtractInventory(locationName, quantity);
                            Log.Information("UI sent order to BL");
                            _orderBL.UpdateOrder(newOrder, location, customer);
                            Console.WriteLine($"Order Sucessfully placed \nOrder ID: {newOrder.OrderID}\n");
                            break;

                        case "N":
                            orderRepeat = false;
                            Log.Information("Customer canceled purchase, Order deleted");
                            _orderBL.DeleteOrder(deleteOrder);
                            break;
                    
                        default:
                            // Invalid Input
                            Console.WriteLine("Please input a valid option");
                            break;
                        }
                    } while (orderRepeat);
            } catch (Exception ex) {
                // Delete order if unsucessful, will auto delete line items
                Log.Information(ex.Message);
                if (orderPlaced) {
                    Log.Information("Order Deleted");
                    _orderBL.DeleteOrder(deleteOrder);
                }
                Console.WriteLine(ex.Message);
            }     
        }
    }
}