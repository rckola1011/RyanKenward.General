using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using RyanKenward.General;
using RyanKenward.General.Controllers;
using RyanKenward.General.Models.Interfaces;
using RyanKenward.General.Models;

namespace RyanKenward.General.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index_ShouldBeViewBagTitleRyanKenward()
        {
            var controller = new HomeController();
            var sut = (ViewResult)controller.Index();
            Assert.AreEqual(sut.ViewBag.Title, "Ryan Kenward: C#");
        }

		[Test]
		public void HelloWorld_ShouldBeHelloWorld()
		{
			HomeController homeController = new HomeController();
			JsonResult sut = (JsonResult)homeController.HelloWorld();
			Assert.AreEqual(sut.Data.ToString(), "Hello world!");
		}
    }
}
