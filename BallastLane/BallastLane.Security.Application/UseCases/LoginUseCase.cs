using BallastLane.Security.Application.Repositories;
using BallastLane.Security.Application.UseCases.Interfaces;
using BallastLane.Security.Domain.Entities;

namespace BallastLane.Security.Application.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _userRepository;

        public LoginUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Authenticate(string loginName, string password)
        {
            User user = await _userRepository.GetByLoginNameAsync(loginName);

            if (user == null || user.Password != password)
            {
                return null;
            }

            return user;
        }
    }
}
