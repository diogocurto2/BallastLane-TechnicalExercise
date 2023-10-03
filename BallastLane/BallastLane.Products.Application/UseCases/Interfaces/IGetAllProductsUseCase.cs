using BallastLane.Products.Application.Repositories.Dtos;

namespace BallastLane.Products.Application.UseCases.Interfaces
{
    public interface IGetAllProductsUseCase
    {
        Task<GetAllProductsOutput> Execute(GetAllProductsInput input);
    }
}