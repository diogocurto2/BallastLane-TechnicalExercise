using BallastLane.Security.Domain.Entities;

namespace BallastLane.Security.Application.UseCases.Interfaces
{
    public interface ILoginUseCase
    {
        Task<User> Authenticate(string loginName, string password);
    }
}