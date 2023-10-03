using BallastLane.Presentation.WebAPI.Controllers.Products;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;

namespace BallastLane.Presentation.WebAPI.UnitTests.Products
{
    [TestFixture]
    public class CreateProductControllerTests
    {
        private CreateProductController _controller;
        private Mock<ICreateProductUseCase> _mockCreateProductUseCase;
        private const string CONTROLER_URL = @"http://localhost:44378/api/products/create";

        [SetUp]
        public void Setup()
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(CONTROLER_URL, UriKind.Absolute)
            };
            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock
                .Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns(CONTROLER_URL)
                .Verifiable();

            _mockCreateProductUseCase = new Mock<ICreateProductUseCase>();
            _controller = new CreateProductController(_mockCreateProductUseCase.Object)
            {
                ControllerContext = controllerContext,
                Url = urlHelperMock.Object
            };
        }

        [Test]
        public async Task Create_WithValidInput_ReturnsCreatedResult()
        {
            // Arrange
            var input = new CreateProductInput(
                "Existing Product",
                19.99m,
                "Description of an existing product");
            var expectedResult = new CreateProductOutput(1);
            _mockCreateProductUseCase.Setup(useCase => useCase.Execute(input))
                .ReturnsAsync(expectedResult);

            // Act
            var response = await _controller.Execute(input) as Created<CreateProductOutput>;
            var responseValue = response.Value as CreateProductOutput;

            // Assert
            Assert.That(response.Location, Is.EqualTo(CONTROLER_URL));
            Assert.That(responseValue.ProductId, Is.EqualTo(expectedResult.ProductId));
        }

        [Test]
        public async Task Create_WithInvalidInput_ReturnsBadRequest()
        {
            // Arrange
            _mockCreateProductUseCase.Setup(useCase => useCase.Execute(null))
                       .ThrowsAsync(new ArgumentException("Invalid input"));

            // Act
            var result = await _controller.Execute(null);

            // Assert
            Assert.IsInstanceOf<BadRequest<string>>(result);
        }

    }
}
