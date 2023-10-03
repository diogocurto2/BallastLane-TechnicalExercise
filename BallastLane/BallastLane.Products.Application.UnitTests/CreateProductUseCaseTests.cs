using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Application.UseCases;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Domain.Entities;
using Moq;

namespace BallastLane.Products.Application.UnitTests
{
    [TestFixture]
    public class CreateProductUseCaseTests
    {
        [Test]
        public async Task Execute_ShouldThrowException_WhenProductNameExists()
        {
            // Arrange
            var input = new CreateProductInput(
                "Existing Product", 
                19.99m, 
                "Description of an existing product");

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetAllAsync(It.Is<GetAllProductsInput>(i => i.Name == input.ProductName)))
                .ReturnsAsync(new GetAllProductsOutput
                (
                    new List<Product>
                    {
                        new Product(1, "Test Product", 19.99m, "Description of a test product")
                    }
                ));
            var createProductUseCase = new CreateProductUseCase(productRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await createProductUseCase.Execute(input));
        }

        [Test]
        public async Task Execute_ShouldCreateProduct_WhenProductNameIsUnique()
        {
            // Arrange
            var input = new CreateProductInput(
                "New Product", 
                19.99m, 
                "Description of a new product");

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetAllAsync(It.Is<GetAllProductsInput>(i => i.Name == input.ProductName)))
                .ReturnsAsync(new GetAllProductsOutput
                (
                    new List<Product>()
                ));
            productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ReturnsAsync(1);

            var createProductUseCase = new CreateProductUseCase(productRepositoryMock.Object);

            // Act
            var resut = await createProductUseCase.Execute(input);

            // Assert
            Assert.That(resut.ProductId, Is.EqualTo(1));
        }
    }
}