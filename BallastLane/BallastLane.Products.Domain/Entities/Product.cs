namespace BallastLane.Products.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public string Description { get; private set; }

        public Product(int id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }
        public Product(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        public void Update(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

    }
}
