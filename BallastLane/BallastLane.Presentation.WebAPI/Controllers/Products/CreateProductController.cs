using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using BallastLane.Products.Presentation.WebAPI.Controllers.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BallastLane.Presentation.WebAPI.Controllers.Products
{
    [ApiController]
    [Route("api/products/create")]
    public class CreateProductController : Controller
    {
        private readonly ICreateProductUseCase _createProductUseCase;

        public CreateProductController(ICreateProductUseCase createProductUseCase)
        {
            _createProductUseCase = createProductUseCase;
        }

        /// <summary>
        /// Use this function to create a product with name, price and description.
        /// </summary>
        /// <param name="input">
        /// For this example, pass data like the example.
        /// </param>
        /// <returns code="200">
        /// Returns Bearer when user and password is valid and product is created
        /// </returns>
        /// <return code="400">
        /// Returns code 400 when parameters is null.
        /// </return>
        /// <returns code="401">
        /// Returns 401 when user is not authorized.
        /// </returns>
        [Authorize]
        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateProductOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IResult> Execute(CreateProductInput input)
        {
            try
            {
                var result = await _createProductUseCase.Execute(input);

                var location = Url.Action(nameof(GetProductByIdController.Execute), new { id = result.ProductId });
                return Results.Created(location, result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

    }
}
