using TaskBLL.Models;

namespace TaskBLL.Interfaces
{
    public interface ITaskService : ICrud<TaskModel>
    {
        Task<IEnumerable<TaskModel>> GetTasksByCategory(int categoryId);
    }
}
