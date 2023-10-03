using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using BallastLane.Products.Domain.Entities;
using BallastLane.Products.Presentation.WebAPI.Controllers.Products;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BallastLane.Presentation.WebAPI.UnitTests.Products
{
    [TestFixture]
    public class GetAllProductsControllerTests
    {
        private GetAllProductsController _controller;
        private Mock<IGetAllProductsUseCase> _mockUseCase;

        [SetUp]
        public void Setup()
        {
            _mockUseCase = new Mock<IGetAllProductsUseCase>();
            _controller = new GetAllProductsController(_mockUseCase.Object);
        }

        [Test]
        public async Task GetAll_WithValidInput_ReturnsOkResult()
        {
            // Arrange
            var input = new GetAllProductsInput { Name = "Product" };
            var expectedResult = new GetAllProductsOutput
            (
                new List<Product>()
            );

            _mockUseCase.Setup(useCase => useCase.Execute(input))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Execute(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetAll_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var input = new GetAllProductsInput { Name = "Product" };
            _mockUseCase.Setup(useCase => useCase.Execute(It.IsAny<GetAllProductsInput>()))
                .ReturnsAsync((GetAllProductsOutput)null);

            // Act
            var result = await _controller.Execute(input);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetAll_InternalError_ReturnsInternalServerError()
        {
            // Arrange
            var input = new GetAllProductsInput { Name = "Product" };
            _mockUseCase.Setup(useCase => useCase.Execute(It.IsAny<GetAllProductsInput>()))
                .Throws(new Exception("Internal server error"));

            // Act
            var result = await _controller.Execute(input);

            // Assert
            Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(500));
        }
    }
}
