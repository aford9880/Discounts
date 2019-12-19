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
            // ����������� - �������� �������������� ��������� ������
            Mock<IDiscountRepository> mock = new Mock<IDiscountRepository>();
            mock.Setup(m => m.GetDiscounts).Returns(new List<Discount> {
                new Discount { ID = 1, Title = "������ 1"},
                new Discount { ID = 2, Title = "������ 2"},
                new Discount { ID = 3, Title = "������ 3"},
                new Discount { ID = 4, Title = "������ 4"},
                new Discount { ID
                = 5, Title = "������ 5"}
            });

            // ����������� - �������� �����������
            controller = new AdminController(mock.Object);            
        }

        [Test]
        public void Index_Contains_All_Discounts() {
            
            // ��������
            result = ((IEnumerable<Discount>)controller.Index().
                ViewData.Model).ToList();
            
            // �����������
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("������ 1", result[0].Title);
            Assert.AreEqual("������ 2", result[1].Title);
            Assert.AreEqual("������ 3", result[2].Title);
        }
    }
}