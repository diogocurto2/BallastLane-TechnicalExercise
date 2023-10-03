using BallastLane.Presentation.WebAPI.Controllers.Products;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallastLane.Presentation.WebAPI.UnitTests.Products
{
    [TestFixture]
    public class DeleteProductControllerTests
    {
        [Test]
        public async Task Execute_ProductDeleted_ReturnsNoContent()
        {
            // Arrange
            var deleteProductUseCaseMock = new Mock<IDeleteProductUseCase>();
            deleteProductUseCaseMock.Setup(useCase => useCase.Execute(It.IsAny<DeleteProductInput>()))
                .ReturnsAsync(new DeleteProductOutput(true));

            var controller = new DeleteProductController(deleteProductUseCaseMock.Object);
            var input = new DeleteProductInput(1);

            // Act
            var result = await controller.Execute(input);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Execute_ProductNotDeleted_ReturnsBadRequest()
        {
            // Arrange
            var deleteProductUseCaseMock = new Mock<IDeleteProductUseCase>();
            deleteProductUseCaseMock.Setup(useCase => useCase.Execute(It.IsAny<DeleteProductInput>()))
                .ReturnsAsync(new DeleteProductOutput(false));

            var controller = new DeleteProductController(deleteProductUseCaseMock.Object);
            var input = new DeleteProductInput(1);

            // Act
            var result = await controller.Execute(input);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Execute_InvalidInput_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var deleteProductUseCaseMock = new Mock<IDeleteProductUseCase>();
            var controller = new DeleteProductController(deleteProductUseCaseMock.Object);
            var input = new DeleteProductInput(0);

            // Act
            var result = await controller.Execute(input);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

    }
}
