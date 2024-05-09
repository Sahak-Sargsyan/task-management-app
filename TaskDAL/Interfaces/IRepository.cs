namespace TaskDAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Get the list of table rows
        Task<IEnumerable<TEntity>> GetAllAsync();

        // Get the specified number of elements from table rows
        Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int rowCount);

        // Get the entity by specified id
        Task<TEntity> GetByIdAsync(int id);

        // Add the entity to table
        Task AddAsync(TEntity entity);

        // Update the entity from table
        Task UpdateAsync(TEntity entity);

        // Deleting specified entity
        Task DeleteAsync(TEntity entity);

        // Deleting the entity by specified id
        Task DeleteByIdAsync(int id);
    }
}
