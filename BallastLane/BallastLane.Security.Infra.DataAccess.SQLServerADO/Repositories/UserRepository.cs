using BallastLane.Security.Application.Repositories;
using BallastLane.Security.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace BallastLane.Security.Infra.DataAccess.SQLServerADO.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Users WHERE Id = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapUserFromDataReader(reader);
                        }
                        return null; // User not found
                    }
                }
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Users", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(MapUserFromDataReader(reader));
                    }
                }
            }

            return users;
        }

        public async Task<int> AddAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(
                    "INSERT INTO Users (Name, Email, LoginName, Password) VALUES (@Name, @Email, @LoginName, @Password); SELECT SCOPE_IDENTITY();",
                    connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@LoginName", user.LoginName);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(
                    "UPDATE Users SET Name = @Name, Email = @Email, LoginName = @LoginName, Password = @Password WHERE Id = @UserId",
                    connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@LoginName", user.LoginName);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DELETE FROM Users WHERE Id = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private User MapUserFromDataReader(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Email = (string)reader["Email"],
                LoginName = (string)reader["LoginName"],
                Password = (string)reader["Password"]
            };
        }

        public async Task<User> GetByLoginNameAsync(string loginName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Users WHERE LoginName = @LoginName";
                    command.Parameters.AddWithValue("@LoginName", loginName);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapUserFromDataReader(reader);
                        }
                    }
                }
            }

            return null;
        }
    }
}
