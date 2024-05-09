using TaskBLL.Models;

namespace TaskBLL.Interfaces
{
    public interface ITaskModel : ICrud<TaskModel>
    {
        Task<IEnumerable<TaskModel>> GetTasksByCategory(int categoryId);
    }
}
