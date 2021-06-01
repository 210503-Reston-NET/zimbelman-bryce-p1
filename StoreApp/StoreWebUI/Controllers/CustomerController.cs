using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBL;
using StoreModels;
using StoreWebUI.Models;

namespace StoreWebUI.Controllers
{
    public class CustomerController : Controller
    {

        private ICustomerBL _customerBL;
        private IOrderBL _orderBL;
        private ILocationBL _locationBL;


        public CustomerController(ICustomerBL customerBL, IOrderBL orderBL, ILocationBL locationBL)
        {
            _customerBL = customerBL;
            _orderBL = orderBL;
            _locationBL = locationBL;
        }


        // GET: Customer
        public ActionResult List(CustomerVM customerVM)
        {
            List<CustomerVM> customerList = new List<CustomerVM>();
            try
            {
                Customer customerModel = _customerBL.SearchCustomer(TempData["firstName"].ToString(), TempData["lastName"].ToString());
                CustomerVM customer = new CustomerVM(customerModel);
                customerList.Add(customer);
                return View(customerList);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }

        // Get
        public ActionResult Index()
        {
            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string firstName, string lastName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["firstName"] = firstName;
                    TempData["lastName"] = lastName;
                    return RedirectToAction(nameof(List));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }


        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVM customerVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _customerBL.AddCustomer(new Customer
                    {
                        FirstName = customerVM.FirstName,
                        LastName = customerVM.LastName,
                        Birthdate = customerVM.Birthdate,
                        PhoneNumber = customerVM.PhoneNumber,
                        Email = customerVM.Email,
                        MailAddress = customerVM.MailAddress
                    }
                        );
                    TempData["firstName"] = customerVM.FirstName;
                    TempData["lastName"] = customerVM.LastName;
                    return RedirectToAction(nameof(List));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new CustomerVM(_customerBL.SearchCustomer(id)));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerVM customerVM, int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer editCustomer = _customerBL.SearchCustomer(id);
                    editCustomer.FirstName = customerVM.FirstName;
                    editCustomer.LastName = customerVM.LastName;
                    editCustomer.Email = customerVM.Email;
                    editCustomer.Birthdate = customerVM.Birthdate;
                    editCustomer.PhoneNumber = customerVM.PhoneNumber;
                    editCustomer.MailAddress = customerVM.MailAddress;
                    _customerBL.EditCustomer(editCustomer);
                    TempData["firstName"] = customerVM.FirstName;
                    TempData["lastName"] = customerVM.LastName;
                    return RedirectToAction(nameof(List));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new CustomerVM(_customerBL.SearchCustomer(id)));
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _customerBL.DeleteCustomer(_customerBL.SearchCustomer(id));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Get
        public ActionResult ViewOrders(int id)
        {
            try
            {
                int i = 0;
                List<OrderVM> orders = _orderBL.GetCustomerOrders(id).Select(ord => new OrderVM(ord)).ToList();
                foreach (OrderVM order in orders)
                {
                    string customerSelector = "customer" + i;
                    Customer customer = _customerBL.SearchCustomer(order.CustomerID);
                    string customerName = customer.FirstName + " " + customer.LastName;
                    ViewData.Add(customerSelector, customerName);

                    string storeSelector = "location" + i;
                    Location location = _locationBL.GetLocationById(order.LocationID);
                    ViewData.Add(storeSelector, location.StoreName);

                    i++;
                }
                return View(orders);
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }
    }
}