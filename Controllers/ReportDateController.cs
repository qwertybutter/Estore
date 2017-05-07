using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estore.Models;

namespace Estore.Controllers
{
    public class ReportDateController : Controller
    {
        //
        // GET: /ReportDate/
        private DataBaseContext db = new DataBaseContext();


        public ActionResult Index(DateTime? startdate, DateTime?enddate)
        {
          
            try
            {

                if (startdate.HasValue&&enddate.HasValue)
                {
                   var  orders = db.Orders.Include(o => o.Product).Include(o => o.Customer).Where(t => t.DateOfBuy >= startdate & t.DateOfBuy <= enddate);
                    ViewBag.Sum = orders.Sum(o => o.QuantityOfProducts);
                                    //need make better 
                    DateTime b = (DateTime)startdate;
                    DateTime c = (DateTime)enddate;
                    ViewBag.Info3 = "Заказы за период с  " + b.ToShortDateString();
                    ViewBag.Info4 = "по  " + c.ToShortDateString() + "  включительно";
                    return View(orders.ToList());
                }

                if(startdate.HasValue&!enddate.HasValue)
                {
                    var orders = db.Orders.Include(o => o.Product).Include(o => o.Customer).Where(t => t.DateOfBuy >= startdate);
                    ViewBag.Sum = orders.Sum(o => o.QuantityOfProducts);
                    DateTime b = (DateTime)startdate;
                    ViewBag.Info3 = "Заказы за период с  " + b.ToShortDateString();
                    return View(orders.ToList());
                }

               if (enddate.HasValue&!startdate.HasValue)
                {
                    var orders = db.Orders.Include(o => o.Product).Include(o => o.Customer).Where(t => t.DateOfBuy <= enddate);
                    ViewBag.Sum = orders.Sum(o => o.QuantityOfProducts);
                    DateTime c = (DateTime)enddate;
                    ViewBag.Info4 = "Заказа по  " + c.ToShortDateString() + "  включительно";
                    return View(orders.ToList());
                }

                else
                {
                    var orders = db.Orders.Include(o => o.Product).Include(o => o.Customer);
                    ViewBag.Sum = db.Orders.Sum(o => o.QuantityOfProducts);
                    return View(orders.ToList());
                }
            }

            catch
            {
                var orders = db.Orders.Include(o => o.Product).Include(o => o.Customer);
                ViewBag.Noz = "Такого заказа нет!";
                return View(orders.ToList());
            }
                //return View("Error"); 
        }


    }
}
