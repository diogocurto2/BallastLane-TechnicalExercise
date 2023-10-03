namespace BallastLane.Products.Application.UseCases.Dtos
{
    public class UpdateProductInput
    {
        public UpdateProductInput(
            int productId,
            string productName,
            decimal productPrice,
            string productDescription)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductDescription = productDescription;
        }

        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public string ProductDescription { get; private set; }
    }
}
