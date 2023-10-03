using BallastLane.Products.Application;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BallastLane.Products.Presentation.WebAPI.Controllers.Products
{
    [ApiController]
    [Route("api/products/getbyid")]
    public class GetProductByIdController : Controller
    {
        private readonly IGetProductByIdUseCase _getProductByIdUseCase;

        public GetProductByIdController(IGetProductByIdUseCase getProductByIdUseCase)
        {
            _getProductByIdUseCase = getProductByIdUseCase;
        }

        /// <summary>
        /// Get product data by Id.
        /// </summary>
        /// <param name="productId">
        /// Id of product
        /// </param>
        /// <response code="200">
        /// When productId parameter is valid, the system returns a type With product data.
        /// </response>
        /// <response code="404">
        /// Retorns 404 when productId parameter is not found.
        /// </response>
        /// <response code="400">
        /// Retorns 400 when input parameter values is not valid.
        /// </response>
        /// <returns code="401">
        /// Returns 401 when user is not authorized.
        /// </returns>
        [Authorize]
        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductByIdOutput))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Execute(int productId)
        {
            try
            {
                var response = await _getProductByIdUseCase.Execute(new GetProductByIdInput(productId));
                if (response == null)
                {
                    return NotFound($"Product with ID {productId} not found.");
                }

                return Ok(response);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
