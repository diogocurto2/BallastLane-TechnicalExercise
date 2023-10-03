using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;

namespace BallastLane.Products.Application.UseCases
{
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetAllProductsOutput> Execute(GetAllProductsInput input)
        {
            var result = await _productRepository.GetAllAsync(input);
            return result;
        }
    }
}

