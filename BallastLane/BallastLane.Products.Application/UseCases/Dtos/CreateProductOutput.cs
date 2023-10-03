namespace BallastLane.Products.Application.UseCases.Dtos
{
    public class CreateProductOutput
    {
        public CreateProductOutput(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; private set; }
    }
}
