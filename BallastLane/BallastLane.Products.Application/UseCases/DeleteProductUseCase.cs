using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;

namespace BallastLane.Products.Application.UseCases
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {

        private readonly IProductRepository _productRepository;

        public DeleteProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<DeleteProductOutput> Execute(DeleteProductInput input)
        {
            if (input.ProductId <= 0)
            {
                throw new ArgumentException("Product ID must be greater than zero.");
            }

            var product = await _productRepository.GetByIdAsync(input.ProductId);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {input.ProductId} not found.");
            }

            await _productRepository.DeleteAsync(input.ProductId);

            return new DeleteProductOutput(true);
        }
    }
}
