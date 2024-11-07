namespace Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}
