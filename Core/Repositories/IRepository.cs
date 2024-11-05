namespace Repositories
{
    public interface IRepository<T>
    {
        T? GetById(string id);
        IEnumerable<T> GetAll();
    }
}
