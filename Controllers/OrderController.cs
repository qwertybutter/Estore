﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estore.Models;
using PagedList;

namespace Estore.Controllers
{
    public class OrderController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Order/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ProdName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CustName = sortOrder == "firstName_asc" ? "firstName_desc" : "firstName_asc";
            ViewBag.QuanProd = sortOrder == "price_asc" ? "price_desc" : "price_asc";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;

            var students = from s in db.Orders.Include(o => o.Product).Include(o => o.Customer)
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Customer.CustomerFirstName.Contains(searchString)
                                       || s.Product.ProductName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "price_asc":
                    students = students.OrderBy(s => s.QuantityOfProducts);
                    break;
                case "price_desc":
                    students = students.OrderByDescending(s => s.QuantityOfProducts);
                    break;
                case "name_desc":
                    students = students.OrderByDescending(s => s.Product.ProductName);
                    break;
                case "firstName_asc":
                    students = students.OrderBy(s => s.Customer.CustomerFirstName);
                    break;
                case "firstName_desc":
                    students = students.OrderByDescending(s => s.Customer.CustomerFirstName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.DateOfBuy);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.DateOfBuy);
                    break;
                default:
                    students = students.OrderBy(s => s.Product.ProductName);
                    break;

            }
            ViewBag.Sum = db.Orders.Sum(o => o.QuantityOfProducts);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        //public ActionResult Index(string firstname = "")
        //{
        //    try
        //    {
                
        //        if (firstname != "")
        //        {
        //            var orders = db.Orders.Include(o => o.Product).Include(o => o.Customer).Where(o => o.Customer.CustomerFirstName.Contains(firstname));
        //            ViewBag.Sum = orders.Sum(o => o.QuantityOfProducts);
        //            ViewBag.Cust = "Согласно поиску" + "  " + '"'+firstname+'"' +" были найдены заказы: ";
        //            return View(orders.ToList());
        //        }

        //        else
        //        {
        //            var orders = db.Orders.Include(o => o.Product).Include(o => o.Customer);
        //            ViewBag.Sum = db.Orders.Sum(o => o.QuantityOfProducts);
        //            return View(orders.ToList());
        //        }
        //    }
        //    catch
        //    {
        //        var orders = db.Orders.Include(o => o.Product).Include(o => o.Customer);
        //        ViewBag.Sum = db.Orders.Sum(o => o.QuantityOfProducts);
        //        ViewBag.Error = "Нет заказа с таким покупателем:  " + "  " + firstname;
        //        return View(orders.ToList());
        //    }
        //}



        //
        // GET: /Order/Details/5

        public ActionResult Details(int id = 0)
        {
            Order order = db.Orders.Find(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductName = db.Products.Single(o => o.Id == order.ProductId);
            ViewBag.CustomerFirstName = db.Customers.Single(o => o.Id == order.CustomerId);
            return View(order);
       }


        //
        // GET: /Order/Create

        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerFirstName");
            return View();
        }

        //
        // POST: /Order/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", order.ProductId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerFirstName", order.CustomerId);
            return View(order);
        }

        //
        // GET: /Order/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
               return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", order.ProductId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerFirstName", order.CustomerId);
            return View(order);
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", order.ProductId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerFirstName", order.CustomerId);
            return View(order);
        }


        //
        // GET: /Order/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductName = db.Products.Single(o => o.Id == order.ProductId);
            ViewBag.CustomerFirstName = db.Customers.Single(o => o.Id == order.CustomerId);
            return View(order);
        }

        //
        // POST: /Order/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}