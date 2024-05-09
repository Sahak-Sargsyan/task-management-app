using Microsoft.EntityFrameworkCore;
using TaskDAL.Entities;
using TaskDAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskDAL.Repositories
{
    public class UserRepository : AbstractRepository, IUserRepository
    {
        private readonly DbSet<User> _users;
        public UserRepository(TaskContext context) : base(context)
        {
            _users = _context.Set<User>();
        }

        public async Task AddAsync(User entity)
        {
            await _users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _users.Remove(entity);
            await _context.SaveChangesAsync();
        }

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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync(int pageNumber, int rowCount)
        {
            return await _users.Skip((pageNumber - 1) * rowCount)
                        .Take(rowCount)
                        .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _users.FindAsync(id);
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), "The user doesn't exist");
            }

            return user;
        }

        public async Task UpdateAsync(User entity)
        {
            _users.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
