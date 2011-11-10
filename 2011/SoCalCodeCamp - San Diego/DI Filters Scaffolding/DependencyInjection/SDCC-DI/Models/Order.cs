using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SDCC_DI.Models
{
    public class Order
    {
        [Required] public string CreditCard { get; set; }
        [Required] public string Address { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
    }
}