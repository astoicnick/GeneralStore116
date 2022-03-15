﻿using GeneralStore116.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace GeneralStore116.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var query = context.Transactions.ToArray();
            return View(query);
        }
        // GET: Transaction/Create
        public ActionResult Create()
        {
            var context = new ApplicationDbContext();
            ViewData["Products"] = context.Products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            }).ToArray();
            ViewData["Customers"] = context.Customers.AsEnumerable().Select(customer => new SelectListItem
            {
                Text = customer.FullName,
                Value = customer.CustomerId.ToString()
            });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction model)
        {
            var context = new ApplicationDbContext();
            ViewData["Products"] = context.Products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });
            ViewData["Customers"] = context.Customers.AsEnumerable().Select(customer => new SelectListItem
            {
                Text = customer.FullName,
                Value = customer.CustomerId.ToString()
            });
            if (context.Products.Find(model.ProductId) == null)
            {
                ViewData["Error"] = "Invalid Product Id";
                return View(model);
            }
            else if (context.Customers.Find(model.CustomerId) == null)
            {
                ViewData["Error"] = "Invalid Customer Id";
                return View(model);
            }
            model.CreatedAt = DateTime.Now;
            context.Transactions.Add(model);
            if (context.SaveChanges() == 1)
            {
                return Redirect("/Transaction");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var context = new ApplicationDbContext();
            // Validate the id, put entity in a variable
            var entity = context.Transactions.Find(id);
            if (entity == null)
            {
                ViewData["Error"] = "Invalid Transaction Id";
                return Redirect("/transaction");
            }
            // Populate the drop downs
            var customers = context.Customers.AsEnumerable();
            var products = context.Products.AsEnumerable();
            ViewData["Customers"] = customers.Select(c => new SelectListItem
            {
                Text = c.FullName,
                Value = c.CustomerId.ToString()
            });
            ViewData["Products"] = products.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ProductId.ToString()
            });
            // Supply the entity to the view
            return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Transaction model)
        {
            var context = new ApplicationDbContext();
            var entity = context.Transactions.Find(id);
            if (entity == null)
            {
                return Redirect("/transaction");
            }
            entity.CustomerId = model.CustomerId;
            entity.ProductId = model.ProductId;
            if (context.SaveChanges() == 1)
            {
                return Redirect("/transaction");
            }
            ViewData["Error"] = "Couldn't update your transaction, my bad bro.";
            return View(model);
        }

    }
}