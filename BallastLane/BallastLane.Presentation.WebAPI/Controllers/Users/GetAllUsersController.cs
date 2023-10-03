using BallastLane.Security.Application.UseCases.Interfaces;
using BallastLane.Security.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BallastLane.Presentation.WebAPI.Controllers.Users
{
    [Route("api/users/getall")]
    [ApiController]
    public class GetAllUsersController : ControllerBase
    {
        private readonly IGetAllUsersUseCase _getAllUsersUseCase;

        public GetAllUsersController(IGetAllUsersUseCase getAllUsersUseCase)
        {
            _getAllUsersUseCase = getAllUsersUseCase ?? throw new ArgumentNullException(nameof(getAllUsersUseCase));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<User> users = await _getAllUsersUseCase.Execute();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
