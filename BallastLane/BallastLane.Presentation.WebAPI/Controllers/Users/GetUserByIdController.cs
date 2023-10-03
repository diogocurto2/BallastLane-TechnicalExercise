using BallastLane.Security.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BallastLane.Presentation.WebAPI.Controllers.Users
{
    [ApiController]
    [Route("api/users/getbyid")]
    public class GetUserByIdController : Controller
    {
        private readonly IGetUserByIdUseCase _getUserByIdUseCase;

        public GetUserByIdController(IGetUserByIdUseCase getUserByIdUseCase)
        {
            _getUserByIdUseCase = getUserByIdUseCase ?? throw new ArgumentNullException(nameof(getUserByIdUseCase));
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await _getUserByIdUseCase.Execute(userId);

                if (user == null)
                {
                    return NotFound($"User with ID {userId} not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Manipule erros ou exceções aqui
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
