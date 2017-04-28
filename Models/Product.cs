using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Estore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Название товара")]
        public string ProductName { get; set; }

        [Display(Name = "Цена")]
        public int ProductPrice { get; set; }

        [Display(Name = "Описание")]
        public string ProductDescription { get; set; }

        [Display(Name = "Дата добавления")]
        [DataType(DataType.Date)]
        public DateTime DateOfAdding { get; set; }
    }
}