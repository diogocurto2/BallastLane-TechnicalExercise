using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Application.UseCases.Dtos;
using BallastLane.Products.Application.UseCases.Interfaces;
using BallastLane.Products.Domain.Entities;

namespace BallastLane.Products.Application.UseCases
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public CreateProductUseCase(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateProductOutput> Execute(CreateProductInput input)
        {
            var result = await _productRepository.GetAllAsync(new GetAllProductsInput(input.ProductName));
            if (result.TotalCount > 0)
            {
                throw new ApplicationException("A product with the same name already exists.");
            }

            var product = new Product(input.ProductName, input.ProductPrice, input.ProductDescription);
            var productId = await _productRepository.AddAsync(product);

            return new CreateProductOutput(productId);
        }
    }
}
