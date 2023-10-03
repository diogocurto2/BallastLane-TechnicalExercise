using BallastLane.Products.Application.UseCases.Dtos;

namespace BallastLane.Products.Application.UseCases.Interfaces
{
    public interface IGetProductByIdUseCase
    {
        Task<GetProductByIdOutput> Execute(GetProductByIdInput input);
    }
}
