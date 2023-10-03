namespace BallastLane.Products.Application.UseCases.Dtos
{
    public class DeleteProductInput
    {
        public DeleteProductInput(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; private set; }
    }
}
