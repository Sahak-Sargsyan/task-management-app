using TaskDAL;
using TaskEntity = TaskDAL.Entities.Task;
using Task = System.Threading.Tasks.Task;
using TaskDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskDAL.Repositories
{
    /// <summary>
    /// Contains operations working with Tasks
    /// </summary>
    public class TaskRepository : AbstractRepository, ITaskRepository
    {
        /// <summary>
        /// DbSet type of <see cref="TaskEntity"/> to work with Tasks table
        /// </summary>
        private readonly DbSet<TaskEntity> _tasks;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskRepository"/> with <see cref="TaskContext"/> dbContext
        /// </summary>
        /// <param name="context">TaskContext dbContext</param>
        public TaskRepository(TaskContext context) : base(context)
        {
            _tasks = _context.Set<TaskEntity>();
        }

        /// <summary>
        /// Adds given <see cref="TaskEntity"/> entity into Tasks table
        /// </summary>
        /// <param name="entity">Task entity</param>
        /// <returns></returns>
        public async Task AddAsync(TaskEntity entity)
        {
            await _tasks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes specified <see cref="TaskEntity"/> entity from table
        /// </summary>
        /// <param name="entity">Task entity</param>
        /// <returns></returns>
        public async Task DeleteAsync(TaskEntity entity)
        {
            _tasks.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes Task with specified id
        /// </summary>
        /// <param name="id">Id of specified Task</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Returns all rows from Tasks table
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _tasks.ToListAsync();
        }

        /// <summary>
        /// Returns the specified number of rows
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TaskEntity>> GetAllAsync(int pageNumber, int rowCount)
        {
            return await _tasks.Skip((pageNumber - 1) * rowCount)
                        .Take(rowCount)
                        .ToListAsync();
        }

        /// <summary>
        /// Returns all Tasks of specified user
        /// </summary>
        /// <param name="userId">Id for user</param>
        /// <returns></returns>
        public async Task<ICollection<TaskEntity>> GetAllByUserId(int userId)
        {
            return await _tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        /// <summary>
        /// Returns all rows with details
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">throws the exception when there is nothing to return</exception>
        public async Task<ICollection<TaskEntity>> GetAllWithDetails()
        {
            var tasks = await _tasks.Include(t => t.User)
                                    .ToListAsync();
            if(tasks == null)
            {
                throw new ArgumentException(nameof(tasks), "The list of tasks is null");
            }

            return tasks;
        }

        /// <summary>
        /// Returns Task with specified id
        /// </summary>
        /// <param name="id">Id for task</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">throws the exception when task doesn't exist</exception>
        public async Task<TaskEntity> GetByIdAsync(int id)
        {
            var task = await _tasks.FindAsync(id);
            if(task == null)
            {
                throw new ArgumentNullException(nameof(task), "The task entity trying to get is null");
            }

            return task;
        }

        /// <summary>
        /// Returns Task with details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throws the exception when task doesn't exist</exception>
        public async Task<TaskEntity> GetByIdWithDetails(int id)
        {
            var entityWithDetails = await _tasks.Include(t => t.User).FirstOrDefaultAsync(task => task.Id == id);
            if(entityWithDetails == null)
            {
                throw new ArgumentNullException(nameof(entityWithDetails), "The entity trying to get with details is null");
            }
            
            return entityWithDetails;
        }

        /// <summary>
        /// Updates the specified <see cref="TaskEntity"/> entity in the table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TaskEntity entity)
        {
            var existingEntity = _tasks.Find(entity.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            await _context.SaveChangesAsync();
        }
    }
}

