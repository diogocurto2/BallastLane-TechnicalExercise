using BallastLane.Security.Domain.Entities;
using BallastLane.Security.Infra.DataAccess.SQLServerADO.Repositories;

namespace BallastLane.Security.Infra.DataAccess.SQLServerADO.UnitTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private const string ConnectionString = "Data Source=localhost,1433;Initial Catalog=BallastLane; MultipleActiveResultSets=true; User ID=sa; Password=Strong@Passw0rd; TrustServerCertificate=true"; 

        [Test]
        public void GetByIdAsync_UserExists_ReturnsUser()
        {
            // Arrange
            int existingUserId = 1;
            var userRepository = new UserRepository(ConnectionString);

            // Act
            User user = userRepository.GetByIdAsync(existingUserId).Result;

            // Assert
            Assert.IsNotNull(user);
            Assert.That(user.Id, Is.EqualTo(existingUserId));
        }

        [Test]
        public void GetByIdAsync_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            int nonExistingUserId = -1;
            var userRepository = new UserRepository(ConnectionString);

            // Act
            User user = userRepository.GetByIdAsync(nonExistingUserId).Result;

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public void GetByLoginNameAsync_UserExists_ReturnsUser()
        {
            // Arrange
            string existingLoginName = "adm";
            var userRepository = new UserRepository(ConnectionString);

            // Act
            User user = userRepository.GetByLoginNameAsync(existingLoginName).Result;

            // Assert
            Assert.IsNotNull(user);
            Assert.That(user.LoginName, Is.EqualTo(existingLoginName));
        }

        [Test]
        public void GetByLoginNameAsync_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            string nonExistingLoginName = "non_existing_user";
            var userRepository = new UserRepository(ConnectionString);

            // Act
            User user = userRepository.GetByLoginNameAsync(nonExistingLoginName).Result;

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public void GetAllAsync_ReturnsListOfUsers()
        {
            // Arrange
            var userRepository = new UserRepository(ConnectionString);

            // Act
            var users = userRepository.GetAllAsync().Result;

            // Assert
            Assert.IsNotNull(users);
            Assert.IsNotEmpty(users);
        }

    }
}