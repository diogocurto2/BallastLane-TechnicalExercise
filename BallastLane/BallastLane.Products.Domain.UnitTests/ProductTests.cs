using BallastLane.Products.Domain.Entities;

namespace BallastLane.Products.Domain.UnitTests
{
    public class ProductTests
    {
        [Test]
        public void Product_Initialization_Succeeds()
        {
            // Arrange
            var expectedName = "Sample Product";
            decimal expectedPrice = 10.99M;
            var expectedDescription = "A sample product description.";

            // Act 
            var product = new Product(
                expectedName,
                expectedPrice,
                expectedDescription
            );


            // Assert
            Assert.NotNull(product);
            Assert.That(product.Id, Is.EqualTo(0));
            Assert.That(product.Name, Is.EqualTo(expectedName));
            Assert.That(product.Price, Is.EqualTo(expectedPrice));
            Assert.That(product.Description, Is.EqualTo(expectedDescription));
        }

        [Test]
        public void Update_ShouldUpdateProductProperties()
        {
            // Arrange
            var product = new Product(1, "Original Product", 19.99m, "Original description");

            // Act
            product.Update("Updated Product", 25.99m, "Updated description");

            // Assert
            Assert.That(product.Name, Is.EqualTo("Updated Product"));
            Assert.That(product.Price, Is.EqualTo(25.99m));
            Assert.That(product.Description, Is.EqualTo("Updated description"));
        }

    }
}