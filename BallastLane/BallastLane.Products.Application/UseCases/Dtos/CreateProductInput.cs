namespace BallastLane.Products.Application.UseCases.Dtos
{
    public class CreateProductInput
    {
        public CreateProductInput(
            string productName,
            decimal productPrice,
            string productDescription)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            ProductDescription = productDescription;
        }

        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public string ProductDescription { get; private set; }
    }
}
