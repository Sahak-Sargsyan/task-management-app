using TaskDAL;
using TaskEntity = TaskDAL.Entities.Task;
using Task = System.Threading.Tasks.Task;
using TaskDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskDAL.Entities;

namespace TaskDAL.Repositories
{
    public class TaskRepository : AbstractRepository, ITaskRepository
    {
        private readonly DbSet<TaskEntity> _tasks;
        public TaskRepository(TaskContext context) : base(context)
        {
            _tasks = _context.Set<TaskEntity>();
        }

        public async Task AddAsync(TaskEntity entity)
        {
            await _tasks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskEntity entity)
        {
            _tasks.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var task = await _tasks.FindAsync(id);
            if(task == null)
            {
                throw new ArgumentNullException(nameof(task), "The task entity trying to delete is null");
            }

            _tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _tasks.ToListAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync(int pageNumber, int rowCount)
        {
            return await _tasks.Skip((pageNumber - 1) * rowCount)
                        .Take(rowCount)
                        .ToListAsync();
        }

        public async Task<TaskEntity> GetByIdAsync(int id)
        {
            var task = await _tasks.FindAsync(id);
            if(task == null)
            {
                throw new ArgumentNullException(nameof(task), "The task entity trying to get is null");
            }

            return task;
        }

        public async Task UpdateAsync(TaskEntity entity)
        {
            _tasks.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
    }
}
