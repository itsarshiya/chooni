using chooni.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task Get_Product_ReturnsProduct()
        {
            // Arrange
            var mockContext = new Mock<ChooniContext>();
            var products = new List<Product>
            {
                new Product { Id = 1, Title = "Product 1" },
                new Product { Id = 2, Title = "Product 2" }
            }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Product>>();
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());
            mockContext.Setup(c => c.Products).Returns(mockDbSet.Object);
            var controller = new ProductController(mockContext.Object);

            // Act
            var result = await controller.GetProducts();

            // Assert
            var productsResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Title = "Product 1" },
                new Product { Id = 2, Title = "Product 2" }
            };
            Assert.Equal(expectedProducts, productsResult.Value);
        }
    }
}
