using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Estore.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Display(Name="Имя покупателя")]
        public string CustomerFirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string CustomerLatName { get; set; }

        [Display(Name = "Дата регистрации")]
        [DataType(DataType.Date)]
        public DateTime DateOfRegistration { get; set; }

        [Display(Name = "Почта")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        public List<Order> Orders { get; set; }
    }
}