using TaskBLL.Interfaces;
using TaskBLL.Models;
using TaskDAL.Interfaces;
using TaskDAL.Repositories;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskDAL.Entities.Task;

namespace TaskBLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task AddAsync(TaskModel model)
        {
            var taskEntity = new TaskEntity(model.Title, model.Description, model.DueDate, model.UserId);

            await _taskRepository.AddAsync(taskEntity);
        }

        public async Task DeleteAsync(int modelId)
        {
            await _taskRepository.DeleteByIdAsync(modelId);
        }

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            var TaskEntityList = await _taskRepository.GetAllWithDetails();
            var taskModelList = new List<TaskModel>();

            foreach (var taskEntity in TaskEntityList)
            {
                taskModelList.Add(new TaskModel(
                    taskEntity.Id, 
                    taskEntity.Title, 
                    taskEntity.Description, 
                    taskEntity.DueDate, 
                    taskEntity.UserId));
            }

            return taskModelList;
        }

        public async Task<IEnumerable<TaskModel>> GetAllByUserId(int userId)
        {
            var tasks = await _taskRepository.GetAllByUserId(userId);
            var taskModelList = new List<TaskModel>();

            if(tasks != null)
            {
                foreach (var task in tasks)
                {
                    taskModelList.Add(new TaskModel(task.Id, task.Title, task.Description, task.DueDate, task.UserId));
                }
            }

            return taskModelList;
        }

        public async Task<TaskModel> GetByIdAsync(int id)
        {
            var taskEntity = await _taskRepository.GetByIdWithDetails(id);
            var taskModel = new TaskModel(taskEntity.Id, taskEntity.Title, taskEntity.Description, dueDate: taskEntity.DueDate, userId: taskEntity.UserId);

            return taskModel;
        }

        public async Task UpdateAsync(TaskModel model)
        {
            var task = new TaskEntity(model.Title, model.Description, model.DueDate, model.UserId) { Id = model.Id };

            await _taskRepository.UpdateAsync(task);
        }
    }
}
