using BallastLane.Security.Domain.Entities;

namespace BallastLane.Security.Application.UseCases.Interfaces
{
    public interface IGetAllUsersUseCase
    {
        Task<List<User>> Execute();
    }
}