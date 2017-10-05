using System;
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
    public class ProductController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Product/

        public ActionResult Index(string sortOrder,string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescrSortParm = sortOrder== "asc_desc"? "desc_desc": "asc_desc";
            ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var students = from s in db.Products
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.ProductName.Contains(searchString)
                                       || s.ProductDescription.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "price_asc":
                    students = students.OrderBy(s => s.ProductPrice);
                    break;
                case "price_desc":
                    students = students.OrderByDescending(s => s.ProductPrice);
                    break;
                case "name_desc":
                    students = students.OrderByDescending(s => s.ProductName);
                    break;
                case "asc_desc":
                    students = students.OrderBy(s => s.ProductDescription);
                    break;
                case "desc_desc":
                    students = students.OrderByDescending(s => s.ProductDescription);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.DateOfAdding);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.DateOfAdding);
                    break;
                default:
                    students = students.OrderBy(s => s.ProductName);
                    break;
                  
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }
        
        
        
        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Product product = db.Products.Find(id);
            if (!db.Orders.Any(t => t.Product.Id == id))
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //  else return View("Error");

           else 
            {
                TempData["notice"] = "Can't delete!";
                return View(product);
            }




        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}