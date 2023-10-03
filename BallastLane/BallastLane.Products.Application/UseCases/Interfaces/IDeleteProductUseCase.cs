using BallastLane.Products.Application.UseCases.Dtos;

namespace BallastLane.Products.Application.UseCases.Interfaces
{
    public interface IDeleteProductUseCase
    {
        Task<DeleteProductOutput> Execute(DeleteProductInput input);
    }
}