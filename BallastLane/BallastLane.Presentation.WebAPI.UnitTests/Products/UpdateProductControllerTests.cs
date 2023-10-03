using BallastLane.Presentation.WebAPI.Controllers.Products;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BallastLane.Presentation.WebAPI.UnitTests.Products
{
    [TestFixture]
    public class UpdateProductControllerTests
    {
        private UpdateProductController _controller;
        private Mock<IUpdateProductUseCase> _mockUpdateProductUseCase;
        private const string CONTROLER_URL = @"http://localhost:44378/api/products/Update";

        [SetUp]
        public void Setup()
        {
            _mockUpdateProductUseCase = new Mock<IUpdateProductUseCase>();
            _controller = new UpdateProductController(_mockUpdateProductUseCase.Object);
        }

        [Test]
        public async Task Update_WithValidInput_ReturnsUpdatedResult()
        {
            // Arrange
            var input = new UpdateProductInput(
                1,
                "Existing Product",
                19.99m,
                "Description of an existing product");
            var expectedResult = new UpdateProductOutput(true);
            _mockUpdateProductUseCase.Setup(useCase => useCase.Execute(input))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Execute(input);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Update_WithInvalidInput_ReturnsBadRequest()
        {
            // Arrange
            _mockUpdateProductUseCase.Setup(useCase => useCase.Execute(null))
                       .ThrowsAsync(new ArgumentException("Invalid input"));

            // Act
            var result = await _controller.Execute(null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

    }
}
