using TaskEntity = TaskDAL.Entities.Task;

namespace TaskDAL.Interfaces
{
    // Should be extended
    public interface ITaskRepository : IRepository<Entities.Task>
    {
        Task<TaskEntity> GetByIdWithDetails(int id);
        Task<ICollection<TaskEntity>> GetAllWithDetails();
        Task<ICollection<TaskEntity>> GetAllByUserId(int userId);

    }
}
