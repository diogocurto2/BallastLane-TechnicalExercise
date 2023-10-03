namespace BallastLane.Products.Application.Repositories.Dtos
{
    public class GetAllProductsInput
    {
        public string Name { get; set; }

        public GetAllProductsInput() { }

        public GetAllProductsInput(string name)
        {
            Name = name;
        }
    }
}
