using BallastLane.Security.Application.Repositories;
using BallastLane.Security.Application.UseCases.Dtos;
using BallastLane.Security.Application.UseCases.Interfaces;
using BallastLane.Security.Domain.Entities;

namespace BallastLane.Security.Application.UseCases
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Execute(CreateUserInput input)
        {
            var user = new User
            {
                Name = input.Name,
                Email = input.Email,
                LoginName = input.LoginName,
                Password = input.Password
            };

            return await _userRepository.AddAsync(user);
        }
    }
}
