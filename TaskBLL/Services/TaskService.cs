using TaskBLL.Interfaces;
using TaskBLL.Models;
using TaskDAL.Repositories;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskDAL.Entities.Task;

namespace TaskBLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskRepository _taskRepository;
        public TaskService(TaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task AddAsync(TaskModel model)
        {
            var taskList = await _taskRepository.GetAllAsync();
            var last = taskList.LastOrDefault();
            var newIndex = last == null ? 1 : last.Id + 1;

            var taskEntity = new TaskEntity(model.Id, model.Title, model.Description, model.DueDate, model.Priority, model.CategoryId, model.UserId);

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
                var taskModel = new TaskModel(
                    taskEntity.Id, 
                    taskEntity.Title, 
                    taskEntity.Description, 
                    taskEntity.DueDate, 
                    taskEntity.Priority, 
                    taskEntity.CategoryId, 
                    taskEntity.UserId);
            }

            return taskModelList;
        }

        public async Task<TaskModel> GetByIdAsync(int id)
        {
            var taskEntity = await _taskRepository.GetByIdWithDetails(id);
            var taskModel = new TaskModel(taskEntity.Id, taskEntity.Title, taskEntity.Description, taskEntity.DueDate, taskEntity.Priority, taskEntity.CategoryId, taskEntity.UserId);

            return taskModel;
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByCategory(int categoryId)
        {
            var taskEnitityList = await _taskRepository.GetAllByCategoryId(categoryId);
            var taskModelList = new List<TaskModel>();

            if (taskEnitityList != null)
            {
                foreach(var taskEntity in taskEnitityList)
                {
                    taskModelList.Add(new TaskModel(taskEntity.Id,taskEntity.Title,taskEntity.Description, taskEntity.DueDate, taskEntity.Priority, taskEntity.Category.Id, taskEntity.UserId));
                }
            }

            return taskModelList;
        }

        public async Task UpdateAsync(TaskModel model)
        {
            var taskToUpdate = await _taskRepository.GetByIdAsync(model.Id);
            if(taskToUpdate == null)
            {
                throw new ArgumentNullException(nameof(model), "No such task to update");
            }

            await _taskRepository.UpdateAsync(taskToUpdate);
        }
    }
}
