namespace TaskBLL.Interfaces
{
    /// <summary>
    /// Basic operations for all services
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface ICrud<TModel> where TModel : class
    {
        /// <summary>
        /// Gets all data for view from specified table
        /// </summary>
        /// <returns>All rows of table</returns>
        Task<IEnumerable<TModel>> GetAllAsync();

        /// <summary>
        /// Gets the item as model
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Model given from repository</returns>
        Task<TModel> GetByIdAsync(int id);

        /// <summary>
        /// Adds an item given as Task model from view
        /// </summary>
        /// <param name="model">Task model given from view</param>
        Task AddAsync(TModel model);

        /// <summary>
        /// Updates an item given from view
        /// </summary>
        /// <param name="model"></param>
        Task UpdateAsync(TModel model);

        /// <summary>
        /// Deletes an item given from view
        /// </summary>
        /// <param name="modelId"></param>
        Task DeleteAsync(int modelId);
    }
}
