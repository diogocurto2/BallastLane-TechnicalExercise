using BallastLane.Products.Application.UseCases.Dtos;

namespace BallastLane.Products.Application.UseCases.Interfaces
{
    public interface ICreateProductUseCase
    {
        Task<CreateProductOutput> Execute(CreateProductInput input);
    }
}