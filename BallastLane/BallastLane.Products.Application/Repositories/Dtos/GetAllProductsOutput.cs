using BallastLane.Products.Domain.Entities;

namespace BallastLane.Products.Application.Repositories.Dtos
{
    public class GetAllProductsOutput
    {
        public List<Product> Products { get; }
        public int TotalCount { get; }

        public GetAllProductsOutput(List<Product> products)
        {
            Products = products;
            TotalCount = products.Count;
        }
    }
}
