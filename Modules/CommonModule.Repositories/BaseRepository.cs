using CommonModule.DataProviders;

namespace CommonModule.Repositories
{
    public class BaseRepository<T>(IDataProvider dataProvider) : IRepository<T>
    {
        private readonly IDataProvider _dataProvider = dataProvider;

        public IEnumerable<T> GetAll()
        {
            return _dataProvider.GetData<T>();
        }
    }
}
