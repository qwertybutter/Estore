using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Estore.Models
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext():base(nameOrConnectionString: "DataContext")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}