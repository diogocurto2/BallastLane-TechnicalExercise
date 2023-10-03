using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Application.UseCases;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Domain.Entities;
using Moq;


namespace BallastLane.Products.Application.UnitTests
{
    [TestFixture]
    public class UpdateProductUseCaseTests
    {
        [Test]
        public async Task Execute_ShouldUpdateProduct_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var input = new UpdateProductInput(
                productId,
                "Updated Product",
                25.99m,
                "Updated description");

            var existingProduct = new Product(
                productId,
                "Original Product",
                19.99m,
                "Original description");

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingProduct);
            productRepositoryMock.Setup(repo => repo.GetAllAsync(It.Is<GetAllProductsInput>(i => i.Name == input.ProductName)))
                .ReturnsAsync(new GetAllProductsOutput
                (
                    new List<Product>()
                ));
            productRepositoryMock.Setup(repo => repo.UpdateAsync(existingProduct)).Returns(Task.CompletedTask);

            var updateProductUseCase = new UpdateProductUseCase(productRepositoryMock.Object);

            // Act
            var result = await updateProductUseCase.Execute(input);

            // Assert
            Assert.IsTrue(result.IsUpdated);
            Assert.That(existingProduct.Name, Is.EqualTo(input.ProductName));
            Assert.That(existingProduct.Price, Is.EqualTo(input.ProductPrice));
            Assert.That(existingProduct.Description, Is.EqualTo(input.ProductDescription));
            productRepositoryMock.Verify(repo => repo.UpdateAsync(existingProduct), Times.Once);
        }

        [Test]
        public void Execute_ShouldThrowArgumentException_WhenProductIdIsInvalid()
        {
            // Arrange
            var productId = 0;
            var input = new UpdateProductInput(
                productId,
                "Updated Product",
                25.99m,
                "Updated description");

            var productRepositoryMock = new Mock<IProductRepository>();
            var updateProductUseCase = new UpdateProductUseCase(productRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(
                async () => await updateProductUseCase.Execute(input));
        }

        [Test]
        public void Execute_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 1;
            var input = new UpdateProductInput(
                productId,
                "Updated Product",
                25.99m,
                "Updated description");

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            var updateProductUseCase = new UpdateProductUseCase(productRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(
                async () => await updateProductUseCase.Execute(input));
        }

        [Test]
        public async Task Execute_ShouldThrowException_WhenProductNameExists()
        {
            // Arrange
            var productId = 1;
            var input = new UpdateProductInput(
                productId,
                "Updated Product",
                25.99m,
                "Updated description");

            var existingProduct = new Product(
                productId,
                "Original Product",
                19.99m,
                "Original description");

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingProduct);
            productRepositoryMock.Setup(repo => repo.GetAllAsync(It.Is<GetAllProductsInput>(i => i.Name == input.ProductName)))
                .ReturnsAsync(new GetAllProductsOutput
                (
                    new List<Product>
                    {
                        new Product(1, "Test Product", 19.99m, "Description of a test product")
                    }
                ));
            var createProductUseCase = new UpdateProductUseCase(productRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await createProductUseCase.Execute(input));
        }
    }
}
