using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SDCC_DI.Services;

namespace SDCC_DI.Controllers
{
    public class HomeController : Controller
    {
        private IMessageService messageService;

        public HomeController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = messageService.GetMessage();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
