namespace TaskDAL.Repositories
{
    public abstract class AbstractRepository
    {
        protected readonly TaskContext _context;

        protected AbstractRepository(TaskContext context)
        {
            _context = context;
        }
    }
}
