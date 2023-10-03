using BallastLane.Security.Application.Repositories;
using BallastLane.Security.Application.UseCases.Interfaces;
using BallastLane.Security.Domain.Entities;

namespace BallastLane.Security.Application.UseCases
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Execute(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
    }
}
