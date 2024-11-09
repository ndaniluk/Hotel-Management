namespace CommonModule.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}
