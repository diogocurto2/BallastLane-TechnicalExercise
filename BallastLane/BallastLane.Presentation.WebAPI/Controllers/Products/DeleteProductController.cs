using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BallastLane.Presentation.WebAPI.Controllers.Products
{
    [Route("api/products/Delete")]
    [ApiController]
    public class DeleteProductController : ControllerBase
    {
        private readonly IDeleteProductUseCase _DeleteProductUseCase;

        public DeleteProductController(IDeleteProductUseCase DeleteProductUseCase)
        {
            _DeleteProductUseCase = DeleteProductUseCase;
        }

        /// <summary>
        /// Use this function to Delete a product with product id.
        /// </summary>
        /// <param name="input">
        /// For this example, pass data like the example.
        /// </param>
        /// <returns code="204">
        /// Returns when user and password is valid and product is Deleted
        /// </returns>
        /// <return code="400">
        /// Returns code 400 when parameters is Invalid.
        /// </return>
        /// <returns code="401">
        /// Returns 401 when user is not authorized.
        /// </returns>
        [Authorize]
        [HttpDelete()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Execute(DeleteProductInput input)
        {
            try
            {
                var result = await _DeleteProductUseCase.Execute(input);
                if (result != null && result.IsDeleted)
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
