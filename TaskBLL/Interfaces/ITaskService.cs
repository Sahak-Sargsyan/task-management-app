using TaskBLL.Models;

namespace TaskBLL.Interfaces
{
    /// <summary>
    /// Defines methods for Task Service
    /// </summary>
    public interface ITaskService : ICrud<TaskModel>
    {
        /// <summary>
        /// Gets all tasks by specified user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User's all tasks</returns>
        Task<IEnumerable<TaskModel>> GetAllByUserId(int userId);
    }
}
