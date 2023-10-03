using BallastLane.Security.Application.Repositories;
using BallastLane.Security.Application.UseCases.Interfaces;
using BallastLane.Security.Domain.Entities;

namespace BallastLane.Security.Application.UseCases
{
    public class GetAllUsersUseCase : IGetAllUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Execute()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
