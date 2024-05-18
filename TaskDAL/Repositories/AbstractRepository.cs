namespace TaskDAL.Repositories
{
    /// <summary>
    /// Defines TaskContext field to work with db
    /// </summary>
    public abstract class AbstractRepository
    {
        protected readonly TaskContext _context;

        protected AbstractRepository(TaskContext context)
        {
            _context = context;
        }
    }
}
