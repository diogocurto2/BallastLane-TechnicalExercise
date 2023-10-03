using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.UseCases;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Domain.Entities;
using Moq;

namespace BallastLane.Products.Application.UnitTests
{
    [TestFixture]
    public class DeleteProductUseCaseTests
    {
        [Test]
        public async Task Execute_ShouldDeleteProduct_WhenProductExists()
        {
            // Arrange
            var input = new DeleteProductInput(1);

            var product = new Product("Test Product", 19.99m, "Description of a test product");
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProductId)).ReturnsAsync(product);

            var deleteProductByIdUseCase = new DeleteProductUseCase(productRepositoryMock.Object);

            // Act
            var result = await deleteProductByIdUseCase.Execute(input);

            // Assert
            Assert.IsTrue(result.IsDeleted);
            productRepositoryMock.Verify(repo => repo.DeleteAsync(input.ProductId), Times.Once);
        }

        [Test]
        public void Execute_ShouldThrowArgumentException_WhenProductIdIsInvalid()
        {
            // Arrange
            var input = new DeleteProductInput(0);

            var productRepositoryMock = new Mock<IProductRepository>();
            var deleteProductByIdUseCase = new DeleteProductUseCase(productRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await deleteProductByIdUseCase.Execute(input));
        }

        [Test]
        public void Execute_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            // Arrange
            var input = new DeleteProductInput(1);

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProductId)).ReturnsAsync((Product)null);

            var deleteProductByIdUseCase = new DeleteProductUseCase(productRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await deleteProductByIdUseCase.Execute(input));
        }
    }
}
