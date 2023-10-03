using BallastLane.Products.Application.Repositories;
using BallastLane.Products.Application.Repositories.Dtos;
using BallastLane.Products.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace BallastLane.Products.Infra.DataAccess.SQLServerADO.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository()
        {
        }

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Products WHERE Id = @ProductId", connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToProduct(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<int> AddAsync(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(
                    "INSERT INTO Products (Name, Price, Description) VALUES (@Name, @Price, @Description); SELECT SCOPE_IDENTITY();",
                    connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Description", product.Description);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task UpdateAsync(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(
                    "UPDATE Products SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @ProductId",
                    connection))
                {
                    command.Parameters.AddWithValue("@ProductId", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Description", product.Description);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DELETE FROM Products WHERE Id = @ProductId", connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<GetAllProductsOutput> GetAllAsync(GetAllProductsInput input)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM Products";

                if (!string.IsNullOrEmpty(input.Name))
                {
                    query += " WHERE Name LIKE @Name";
                }

                using (var command = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(input.Name))
                    {
                        command.Parameters.AddWithValue("@Name", $"%{input.Name}%");
                    }

                    var products = new List<Product>();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var product = MapToProduct(reader);
                            products.Add(product);
                        }
                    }

                    return new GetAllProductsOutput
                    (
                        products
                    );
                }
            }
        }

        private Product MapToProduct(SqlDataReader reader)
        {
            return new Product
            (
                reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("Name")),
                reader.GetDecimal(reader.GetOrdinal("Price")),
                reader.GetString(reader.GetOrdinal("Description"))
            );
        }
    }
}
