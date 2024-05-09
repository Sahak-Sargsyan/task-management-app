using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskDAL.Entities;
using TaskDAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskDAL.Repositories
{
    public class CategoryRepository : AbstractRepository, ICategoryRepository
    {
        private readonly DbSet<Category> _categories;
        public CategoryRepository(TaskContext context) : base(context)
        {
            this._categories = _context.Categories;
        }

        public async Task AddAsync(Category entity)
        {
            await _categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category entity)
        {
            _categories.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _categories.FindAsync(id);
            if(entity != null)
            {
                _categories.Remove(entity);
                await _context.SaveChangesAsync();
            }   
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync(int pageNumber, int rowCount)
        {
            return await _categories.Skip((pageNumber - 1) * rowCount)
                        .Take(rowCount)
                        .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _categories.FindAsync(id);
            if(category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return category;
        }

        public async Task UpdateAsync(Category entity)
        {
            _categories.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
