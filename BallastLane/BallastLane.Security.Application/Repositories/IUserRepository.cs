using BallastLane.Security.Application.Repositories.Dtos;
using BallastLane.Security.Domain.Entities;

namespace BallastLane.Security.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int UserId);
        Task<List<User>> GetAllAsync();
        Task<int> AddAsync(User User);
        Task UpdateAsync(User User);
        Task DeleteAsync(int UserId);
        Task<User> GetByLoginNameAsync(string loginName);
    }
}
