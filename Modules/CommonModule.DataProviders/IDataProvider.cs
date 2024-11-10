namespace CommonModule.DataProviders
{
    public interface IDataProvider
    {
        IEnumerable<T> GetData<T>();
    }
}
