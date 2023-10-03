using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;

namespace BallastLane.Products.Application.UseCases
{
    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {

        private readonly IProductRepository _productRepository;

        public GetProductByIdUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductByIdOutput> Execute(GetProductByIdInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("input must be valid.");
            }

            if (input.ProductId <= 0)
            {
                throw new ArgumentException("Product ID must be greater than zero.");
            }

            var product = await _productRepository.GetByIdAsync(input.ProductId);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {input.ProductId} not found.");
            }

            return new GetProductByIdOutput(
                product.Id,
                product.Name,
                product.Price,
                product.Description
            );
        }
    }
}
