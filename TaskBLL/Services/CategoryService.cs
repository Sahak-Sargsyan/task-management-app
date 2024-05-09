using TaskBLL.Interfaces;
using TaskBLL.Models;
using TaskDAL.Entities;
using TaskDAL.Repositories;
using Task = System.Threading.Tasks.Task;

namespace TaskBLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task AddAsync(CategoryModel model)
        {
            var categoryList = await _categoryRepository.GetAllAsync();
            var last = categoryList.LastOrDefault();
            var newIndex = last == null ? 1 : last.Id + 1;

            var category = new Category(newIndex, model.Name);
            await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteAsync(int modelId)
        {
            var category = await _categoryRepository.GetByIdAsync(modelId);
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            var categoryList = await _categoryRepository.GetAllAsync();
            var categoryModelList = new List<CategoryModel>();

            foreach (var category in categoryList)
            {
                categoryModelList.Add(new CategoryModel(category.Id, category.Name));
            }

            return categoryModelList;
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return new CategoryModel(category.Id, category.Name);
        }

        public async Task UpdateAsync(CategoryModel model)
        {
            var category = await _categoryRepository.GetByIdAsync(model.Id);

            category.Name = model.Name;
            
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
