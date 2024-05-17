using Microsoft.EntityFrameworkCore;
using TaskDAL.Entities;
using TaskDAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskDAL.Repositories
{
    /// <summary>
    /// Contains operations working with Users
    /// </summary>
    public class UserRepository : AbstractRepository, IUserRepository
    {
        /// <summary>
        /// DbSet field type of <see cref="User"/> to work with Users table
        /// </summary>
        private readonly DbSet<User> _users;

        /// <summary>
        /// Initializes a new instance of <see cref="UserRepository"/> with <see cref="TaskContext"/> dbContext
        /// </summary>
        /// <param name="context">dbContext parameter</param>
        public UserRepository(TaskContext context) : base(context)
        {
            _users = _context.Set<User>();
        }

        /// <summary>
        /// Adds the specified <see cref="User"/> entity to Users table
        /// </summary>
        /// <param name="entity">User entity</param>
        /// <returns></returns>
        public async Task AddAsync(User entity)
        {
            await _users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the specified <see cref="User"/> entity from Users table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(User entity)
        {
            _users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the user given by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _users.FindAsync(id);
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "The User entity trying to delete is null");
            }

            _users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns all rows of Users table
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _users.ToListAsync();
        }

        /// <summary>
        /// Returns specified count rows of Users table
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllAsync(int pageNumber, int rowCount)
        {
            return await _users.Skip((pageNumber - 1) * rowCount)
                        .Take(rowCount)
                        .ToListAsync();
        }

        /// <summary>
        /// Returns specified count rows from Users table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ICollection<User>> GetAllWithDetails()
        {
            var users = await _users.Include(u => u.Tasks).ToListAsync();
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users), "users not found");
            }

            return users;
        }

        /// <summary>
        /// Returns user with specified id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">throws the exception when there is nothing to return</exception>
        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _users.FindAsync(id);
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), "The user doesn't exist");
            }

            return user;
        }

        /// <summary>
        /// Returns the user with details
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<User> GetByIdWithDetails(int id)
        {
            var user = await _users.Include(u => u.Tasks).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is not found");
            }

            return user;
        }

        /// <summary>
        /// Returns user by specified user name
        /// </summary>
        /// <param name="userName">Users's name</param>
        /// <returns></returns>
        public async Task<User> GetUserByUserName(string userName)
        {
            return await _users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        /// <summary>
        /// Updates the specified <see cref="User"/> entity in the table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(User entity)
        {
            _users.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
