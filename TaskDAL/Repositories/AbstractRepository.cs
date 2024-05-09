namespace TaskDAL.Repositories
{
    // Abstract repository that is inherited by all Repository classes
    public abstract class AbstractRepository
    {
        protected readonly TaskContext _context;

        protected AbstractRepository(TaskContext context)
        {
            _context = context;
        }
    }
}
