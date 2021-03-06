﻿using System.Net;
using System.Web.Mvc;
using MvcSalesApp.Data;

namespace MvcSalesApp.Controllers
{
    public class CustomersWithOrdersController : Controller
    {
        private CustomerWithOrdersData repo = new CustomerWithOrdersData();

        // GET: CustomersWithOrders
        public ActionResult Index()
        {
            return View(repo.GetAllCustomers());
        }

        // GET: CustomersWithOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = repo.FindCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
    }
}
