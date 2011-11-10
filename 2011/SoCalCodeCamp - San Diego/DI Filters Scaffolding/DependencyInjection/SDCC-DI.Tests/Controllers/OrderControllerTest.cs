using SDCC_DI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SDCC_DI.Services;
using SDCC_DI.Models;
using System.Web.Mvc;

namespace SDCC_DI.Tests.Controllers
{
    [TestClass()]
    public class OrderControllerTest
    {
        [TestMethod()]
        public void Can_Order_With_Valid_Credit_Info()
        {
            // Arrange
            ICreditCardService service = new TestCreditCardService();
            OrderController controller = new OrderController(service);

            // Act
            Order order = new Order { CreditCard = "4111111111111111", Price = 50 };
            ActionResult result = controller.Create(order) as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("OrderComplete", redirectResult.RouteValues["action"]);
        }

        [TestMethod()]
        public void Order_Fails_With_Invalid_Credit_Info()
        {
            // Arrange
            ICreditCardService service = new TestCreditCardService();
            OrderController controller = new OrderController(service);

            // Act
            Order order = new Order { CreditCard = "00000000000000000000", Price = 50 };
            ActionResult result = controller.Create(order) as ViewResult;

            // Assert
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(order, viewResult.ViewData.Model);
        }

        [TestMethod()]
        public void Order_Fails_When_Price_Too_High()
        {
            // Arrange
            ICreditCardService service = new TestCreditCardService();
            OrderController controller = new OrderController(service);

            // Act
            Order order = new Order { CreditCard = "00000000000000000000", Price = 5000 };
            ActionResult result = controller.Create(order) as ViewResult;

            // Assert
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(order, viewResult.ViewData.Model);
        }

    }
}
