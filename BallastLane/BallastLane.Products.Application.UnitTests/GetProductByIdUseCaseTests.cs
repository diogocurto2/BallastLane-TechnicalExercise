using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.UseCases;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Domain.Entities;
using Moq;

namespace BallastLane.Products.Application.UnitTests
{
    [TestFixture]
    public class GetProductByIdUseCaseTests
    {
        [Test]
        public async Task Execute_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var input = new GetProductByIdInput(1);

            var product = new Product(1, "Test Product", 19.99m, "Description of a test product");
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            var getProductByIdUseCase = new GetProductByIdUseCase(productRepositoryMock.Object);

            // Act
            var result = await getProductByIdUseCase.Execute(input);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.ProductId, Is.EqualTo(1));
            Assert.That(result.ProductName, Is.EqualTo("Test Product"));
            Assert.That(result.ProductPrice, Is.EqualTo(19.99m));
            Assert.That(result.ProductDescription, Is.EqualTo("Description of a test product"));
        }

        [Test]
        public void Execute_ShouldThrowArgumentException_WhenProductIdIsInvalid()
        {
            // Arrange
            var input = new GetProductByIdInput(0);

            var productRepositoryMock = new Mock<IProductRepository>();
            var getProductByIdUseCase = new GetProductByIdUseCase(productRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await getProductByIdUseCase.Execute(input));
        }

        [Test]
        public void Execute_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            // Arrange
            var input = new GetProductByIdInput(1);

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

            var getProductByIdUseCase = new GetProductByIdUseCase(productRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await getProductByIdUseCase.Execute(input));
        }
    }
}
