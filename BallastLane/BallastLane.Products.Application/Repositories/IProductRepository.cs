using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Domain.Entities;

namespace BallastLane.Products.Application.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int productId);
        Task<GetAllProductsOutput> GetAllAsync(GetAllProductsInput input);
        Task<int> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int productId);
    }
}
