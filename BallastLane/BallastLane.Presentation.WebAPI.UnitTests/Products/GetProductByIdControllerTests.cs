using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using BallastLane.Products.Presentation.WebAPI.Controllers.Products;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BallastLane.Presentation.WebAPI.UnitTests.Products
{
    [TestFixture]
    public class GetProductByIdControllerTests
    {
        private GetProductByIdController _controller;
        private Mock<IGetProductByIdUseCase> _mockUseCase;

        [SetUp]
        public void Setup()
        {
            _mockUseCase = new Mock<IGetProductByIdUseCase>();
            _controller = new GetProductByIdController(_mockUseCase.Object);
        }

        [Test]
        public async Task Get_ProductExists_ReturnsOk()
        {
            // Arrange
            var productId = 1;
            var productDto = new GetProductByIdOutput(1, "Product 1", 19.99M, "Description");

            _mockUseCase.Setup(useCase => useCase.Execute(It.IsAny<GetProductByIdInput>()))
                .ReturnsAsync(productDto);

            // Act
            var result = await _controller.Execute(productId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Get_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var productId = 2;
            _mockUseCase.Setup(useCase => useCase.Execute(It.IsAny<GetProductByIdInput>()))
                .ReturnsAsync((GetProductByIdOutput)null);

            // Act
            var result = await _controller.Execute(productId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task Get_InternalError_ReturnsInternalServerError()
        {
            // Arrange
            var productId = 3;
            _mockUseCase.Setup(useCase => useCase.Execute(It.IsAny<GetProductByIdInput>()))
                .Throws(new Exception("Internal server error"));

            // Act
            var result = await _controller.Execute(productId);

            // Assert
            Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(500));
        }
    }
}
