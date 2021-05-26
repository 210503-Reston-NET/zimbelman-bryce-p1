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

        public ICustomerBL _customerBL;
        public CustomerController(ICustomerBL customerBL)
        {
            _customerBL = customerBL;
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

        public ActionResult Index()
        {
            return View();
        }

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
    }
}