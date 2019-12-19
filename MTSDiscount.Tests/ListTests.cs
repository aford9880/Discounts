using Moq;
using MTSDiscount.Core.Interfaces;
using MTSDiscount.Core.Models;
using MTSDiscount.Web.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MTSDiscount.Tests {
    public class ListTests {

        List<Discount> result;
        DiscountsController controller;

        [SetUp]
        public void Setup() {
            // Организация - создание имитированного хранилища данных
            Mock<IDiscountRepository> mock = new Mock<IDiscountRepository>();
            mock.Setup(m => m.GetDiscounts).Returns(new List<Discount> {
                new Discount { ID = 1, Title = "Скидка 1"},
                new Discount { ID = 2, Title = "Скидка 2"},
                new Discount { ID = 3, Title = "Скидка 3"},
                new Discount { ID = 4, Title = "Скидка 4"},
                new Discount { ID = 5, Title = "Скидка 5"}
            });

            // Организация - создание контроллера
            controller = new DiscountsController(mock.Object);
            controller.pageSize = 3;
        }

        [Test]
        public void Can_Paginate() {

            // Действие
            result = ((IEnumerable<Discount>)controller.Index(2).
                ViewData.Model).ToList();

            // Утверждение (assert)
            List<Discount> discounts = result.ToList();
            Assert.IsTrue(discounts.Count == 2);
            Assert.AreEqual(discounts[0].Title, "Скидка 4");
            Assert.AreEqual(discounts[1].Title, "Скидка 5");
        }
    }
}