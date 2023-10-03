using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;

namespace BallastLane.Products.Application.UseCases
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductUseCase(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<UpdateProductOutput> Execute(UpdateProductInput input)
        {
            if (input.ProductId <= 0)
            {
                throw new ArgumentException("Product ID must be greater than zero.");
            }

            var existingProduct = await _productRepository.GetByIdAsync(input.ProductId);
            if (existingProduct == null)
            {
                throw new NotFoundException($"Product with ID {input.ProductId} not found.");
            }

            var result = await _productRepository.GetAllAsync(new GetAllProductsInput(input.ProductName));
            if (result.TotalCount > 0)
            {
                throw new ApplicationException("A product with the same name already exists.");
            }

            // Atualize os campos do produto com os novos valores usando o método público de atualização
            existingProduct.Update(input.ProductName, input.ProductPrice, input.ProductDescription);

            // Salve as alterações no repositório
            await _productRepository.UpdateAsync(existingProduct);

            return new UpdateProductOutput(true);
        }
    }
}
