using BallastLane.Security.Application.UseCases.Dtos;

namespace BallastLane.Security.Application.UseCases.Interfaces
{
    public interface ICreateUserUseCase
    {
        Task<int> Execute(CreateUserInput input);
    }
}