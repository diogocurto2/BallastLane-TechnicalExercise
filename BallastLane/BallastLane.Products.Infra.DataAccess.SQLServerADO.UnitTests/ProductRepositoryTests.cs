using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Domain.Entities;
using BallastLane.Products.Infra.DataAccess.SQLServerADO.Repositories;

namespace BallastLane.Products.Infra.DataAccess.SQLServerADO.UnitTests
{
    public class Tests
    {
        [TestFixture]
        public class ProductRepositoryTests
        {
            private const string ConnectionString = 
                "Data Source=localhost,1433;Initial Catalog=BallastLane; MultipleActiveResultSets=true; User ID=sa; Password=Strong@Passw0rd; TrustServerCertificate=true";

            [Test]
            public async Task GetByIdAsync_ProductExists_ReturnsProduct()
            {
                // Arrange
                int existingProductId = 1;
                var productRepository = new ProductRepository(ConnectionString);

                // Act
                Product product = await productRepository.GetByIdAsync(existingProductId);

                // Assert
                Assert.IsNotNull(product);
                Assert.AreEqual(existingProductId, product.Id);
            }

            [Test]
            public async Task GetByIdAsync_ProductDoesNotExist_ReturnsNull()
            {
                // Arrange
                int nonExistingProductId = -1;
                var productRepository = new ProductRepository(ConnectionString);

                // Act
                Product product = await productRepository.GetByIdAsync(nonExistingProductId);

                // Assert
                Assert.IsNull(product);
            }


            [Test]
            public async Task GetAllAsync_WithNameFilter_ReturnsFilteredProducts()
            {
                // Arrange
                string filterName = "Product"; 
                var productRepository = new ProductRepository(ConnectionString);
                var input = new GetAllProductsInput
                {
                    Name = filterName
                };

                // Act
                GetAllProductsOutput output = await productRepository.GetAllAsync(input);

                // Assert
                Assert.IsNotNull(output);
                Assert.IsNotEmpty(output.Products);
            }

            [Test]
            public async Task GetAllAsync_WithInvalidNameFilter_ReturnsEmptyList()
            {
                // Arrange
                string invalidFilterName = "InvalidProduct"; 
                var productRepository = new ProductRepository(ConnectionString);
                var input = new GetAllProductsInput
                {
                    Name = invalidFilterName
                };

                // Act
                GetAllProductsOutput output = await productRepository.GetAllAsync(input);

                // Assert
                Assert.IsNotNull(output);
                Assert.IsEmpty(output.Products);
            }

        }
    }
}