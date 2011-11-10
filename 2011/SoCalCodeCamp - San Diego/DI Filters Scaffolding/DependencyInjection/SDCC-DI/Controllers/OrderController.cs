using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDCC_DI.Models;
using SDCC_DI.Services;

namespace SDCC_DI.Controllers
{
    public class OrderController : Controller
    {
        public ICreditCardService service;

        public OrderController(ICreditCardService service)
        {
            this.service = service;
        }

        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Order/Details/5

        public ActionResult OrderComplete()
        {
            return View();
        }

        //
        // GET: /Order/Create

        public ActionResult Create()
        {
            var order = new Order { ProductName = "Darth Vader mask", Price = 20.00m };
            return View(order);
        } 

        //
        // POST: /Order/Create

        [HttpPost]
        public ActionResult Create(Order order)
        {
            try
            {
                bool result = service.ChargeCreditCart(order.CreditCard, order.Price);

                if (result == false)
                {
                    ModelState.AddModelError("CreditCard", "Invalid Credit Card");
                    return View(order);
                }

                return RedirectToAction("OrderComplete");
            }
            catch
            {
                return View(order);
            }
        }
        
    }
}
