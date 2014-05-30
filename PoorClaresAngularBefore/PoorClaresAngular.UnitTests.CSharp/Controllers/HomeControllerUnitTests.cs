using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoorClaresAngular.Controllers;
using PoorClaresAngular.Models;

namespace PoorClaresAngular.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerUnitTests
    {
        private HomeController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new HomeController();
        }

        [TestMethod]
        public void Index_should_return_a_ViewResult()
        {
            var result = _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
