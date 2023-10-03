using BallastLane.Security.Domain.Entities;

namespace BallastLane.Security.Application.Repositories.Dtos
{
    public class GetAllUsersOutput
    {
        public List<User> Users { get; }
        public int TotalCount { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetAllUsersOutput(List<User> users, int totalCount, int pageNumber, int pageSize)
        {
            Users = users;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
