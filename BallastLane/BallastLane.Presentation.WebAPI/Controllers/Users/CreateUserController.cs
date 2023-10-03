using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Presentation.WebAPI.Controllers.Products;
using BallastLane.Security.Application.UseCases.Dtos;
using BallastLane.Security.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BallastLane.Presentation.WebAPI.Controllers.Users
{
    [Route("api/users/create")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;

        public CreateUserController(ICreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        /// <summary>
        /// Use this function to create a user.
        /// </summary>
        /// <param name="input">
        /// For this example, pass data like the example.
        /// </param>
        /// <returns code="200">
        /// Returns code 200 when user and password is valid and product is created
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IResult> Execute([FromBody] CreateUserInput input)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            try
            {
                int userId = await _createUserUseCase.Execute(input);
                var location = Url.Action(nameof(GetProductByIdController.Execute), new { id = userId });
                return Results.Created(location, userId);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
