using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using StoreBL;
using StoreDL;
using StoreModels;
using StoreWebUI.Controllers;
using StoreWebUI.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StoreTests
{
    public class ProductControllerTest
    {

        [Fact]
        public void ProductControllerIndexShouldReturnList()
        {
            var mockBL = new Mock<IProductBL>();
            mockBL.Setup(x => x.GetAllProducts()).Returns(
                new List<Product>()
                {
                    new Product("Frost", 3.49, "Frost?"),
                    new Product("Mocha", 1.99, "Mocha?")
                }
                );
            var controller = new ProductController(mockBL.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<ProductVM>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }
    }
}
