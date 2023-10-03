namespace BallastLane.Products.Application.UseCases.Dtos
{
    public class GetProductByIdInput
    {
        public GetProductByIdInput()
        {
        }
        public GetProductByIdInput(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; set; }
    }
}
