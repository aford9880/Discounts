using Moq;
using MTSDiscount.Core.Interfaces;
using MTSDiscount.Core.Models;
using MTSDiscount.Web.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MTSDiscount.Tests {
    public class AdminTests {

        List<Discount> result;
        AdminController controller;

        [SetUp]
        public void Setup() {
            // Организация - создание имитированного хранилища данных
            Mock<IDiscountRepository> mock = new Mock<IDiscountRepository>();
            mock.Setup(m => m.GetDiscounts).Returns(new List<Discount> {
                new Discount { ID = 1, Title = "Скидка 1"},
                new Discount { ID = 2, Title = "Скидка 2"},
                new Discount { ID = 3, Title = "Скидка 3"},
                new Discount { ID = 4, Title = "Скидка 4"},
                new Discount { ID
                = 5, Title = "Скидка 5"}
            });

            // Организация - создание контроллера
            controller = new AdminController(mock.Object);            
        }

        [Test]
        public void Index_Contains_All_Discounts() {
            
            // Действие
            result = ((IEnumerable<Discount>)controller.Index().
                ViewData.Model).ToList();
            
            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Скидка 1", result[0].Title);
            Assert.AreEqual("Скидка 2", result[1].Title);
            Assert.AreEqual("Скидка 3", result[2].Title);
        }
    }
}