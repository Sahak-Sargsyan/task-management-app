using TaskDAL.Entities;

namespace TaskDAL.Interfaces
{
    // Can be extended
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUserName(string userName);
        Task<ICollection<User>> GetAllWithDetails();
        Task<User> GetByIdWithDetails(int id);
    }
}
