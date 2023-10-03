using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BallastLane.Presentation.WebAPI.Controllers.Products
{
    [Route("api/products/update")]
    [ApiController]
    public class UpdateProductController : ControllerBase
    {
        private readonly IUpdateProductUseCase _updateProductUseCase;

        public UpdateProductController(IUpdateProductUseCase updateProductUseCase)
        {
            _updateProductUseCase = updateProductUseCase;
        }

        /// <summary>
        /// Use this function to update a product with name, price and description.
        /// </summary>
        /// <param name="input">
        /// For this example, pass data like the example.
        /// </param>
        /// <returns code="204">
        /// Returns code 200 when user and password is valid and product is updated
        /// </returns>
        /// <return code="400">
        /// Returns code 400 when parameters is null.
        /// </return>
        /// <returns code="401">
        /// Returns 401 when user is not authorized.
        /// </returns>
        [Authorize]
        [HttpPut()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Execute(UpdateProductInput input)
        {
            try
            {
                var result = await _updateProductUseCase.Execute(input);
                if (result != null && result.IsUpdated)
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
