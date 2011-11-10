using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDCC_DI;
using SDCC_DI.Controllers;
using SDCC_DI.Services;

namespace SDCC_DI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            IMessageService service = new TestMessageService();
            HomeController controller = new HomeController(service);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Test message", result.ViewBag.Message);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            IMessageService service = new TestMessageService();
            HomeController controller = new HomeController(service);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
