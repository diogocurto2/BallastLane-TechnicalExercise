using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Application.UseCases;
using BallastLane.Products.Domain.Entities;
using Moq;

namespace BallastLane.Products.Application.UnitTests
{
    [TestFixture]
    public class GetAllProductsUseCaseTests
    {

        [Test]
        public async Task GetAllProducts_WithInput_ReturnsPagedProducts()
        {
            // Arrange

            var input = new GetAllProductsInput ("Product");
            var expectedProducts = new List<Product>
            {
                new Product ("Product 1", 19.99m, "Description"),
                new Product ("Product 2", 29.99m, "Description")
            };
            var totalCount = expectedProducts.Count;
            var mockRepository = new Mock<IProductRepository>();
            var useCase = new GetAllProductsUseCase(mockRepository.Object);
            mockRepository.Setup(repo => repo.GetAllAsync(input))
                .ReturnsAsync(new GetAllProductsOutput
                (
                    expectedProducts
                ));

            // Act
            var output = await useCase.Execute(input);

            // Assert
            Assert.IsNotNull(output);
            Assert.That(output.Products.Count, Is.EqualTo(expectedProducts.Count));
            Assert.That(output.TotalCount, Is.EqualTo(totalCount));

        }

        [Test]
        public async Task GetAllProducts_WithNoInput_ReturnsAllProducts()
        {
            // Arrange
            var input = new GetAllProductsInput(null);
            var expectedProducts = new List<Product>
            {
                new Product ("Product 1", 19.99m, "Description"),
                new Product ("Product 2", 29.99m, "Description")
            };
            var totalCount = expectedProducts.Count;
            var mockRepository = new Mock<IProductRepository>();
            var useCase = new GetAllProductsUseCase(mockRepository.Object);
            mockRepository.Setup(repo => repo.GetAllAsync(input))
                .ReturnsAsync(new GetAllProductsOutput
                (
                    expectedProducts
                ));

            // Act
            var output = await useCase.Execute(input);

            // Assert
            Assert.IsNotNull(output);
            Assert.That(output.Products.Count, Is.EqualTo(expectedProducts.Count));
            Assert.That(output.TotalCount, Is.EqualTo(totalCount));
        }
    }
}
