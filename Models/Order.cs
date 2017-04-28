using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Estore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }


        [Display(Name = "Дата покупки")]
        [DataType(DataType.Date)]
        public DateTime DateOfBuy { get; set; }

         [Display(Name = "Количество единиц")]
        public int QuantityOfProducts { get; set; }

       
        public Product Product { get; set; }

      
        public Customer Customer { get; set; }
    }
}