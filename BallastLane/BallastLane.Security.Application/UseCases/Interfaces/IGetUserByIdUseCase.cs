using BallastLane.Security.Domain.Entities;

namespace BallastLane.Security.Application.UseCases.Interfaces
{
    public interface IGetUserByIdUseCase
    {
        Task<User> Execute(int userId);
    }
}