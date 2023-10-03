using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BallastLane.Products.Presentation.WebAPI.Controllers.Products
{
    [ApiController]
    [Route("api/products/getall")]
    public class GetAllProductsController : Controller
    {
        private readonly IGetAllProductsUseCase _GetAllProductsUseCase;

        public GetAllProductsController(IGetAllProductsUseCase GetAllProductsUseCase)
        {
            _GetAllProductsUseCase = GetAllProductsUseCase;
        }

        /// <summary>
        /// Get All products
        /// </summary>
        /// <param name="input">
        /// name of product
        /// page size
        /// page number
        /// </param>
        /// <response code="200">
        /// When input parameter is valid, the system returns a type With list of product data.
        /// </response>
        /// <response code="404">
        /// Retorns 404 when input parameter is not found.
        /// </response>
        /// <response code="400">
        /// Retorns 400 when input parameter values is not valid.
        /// </response>
        /// <returns code="401">
        /// Returns 401 when user is not authorized.
        /// </returns>
        [Authorize]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllProductsOutput))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Execute([FromQuery] GetAllProductsInput input)
        {
            try
            {
                var response = await _GetAllProductsUseCase.Execute(input);
                if (response == null)
                {
                    return NotFound($"Results not found.");
                }

                return Ok(response);
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
