using TaskBLL.Models;

namespace TaskBLL.Interfaces
{
    /// <summary>
    /// Provides methods for User Service
    /// </summary>
    public interface IUserService : ICrud<UserModel>
    {
        /// <summary>
        /// Validates the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Logged in user</returns>
        Task<UserModel> ValidateUserAsync(string username, string password); 
    }
}
