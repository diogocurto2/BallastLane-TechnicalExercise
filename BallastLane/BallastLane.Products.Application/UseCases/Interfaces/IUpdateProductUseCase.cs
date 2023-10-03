using BallastLane.Products.Application.UseCases.Dtos;

namespace BallastLane.Products.Application.UseCases.Interfaces
{
    public interface IUpdateProductUseCase
    {
        Task<UpdateProductOutput> Execute(UpdateProductInput input);
    }
}