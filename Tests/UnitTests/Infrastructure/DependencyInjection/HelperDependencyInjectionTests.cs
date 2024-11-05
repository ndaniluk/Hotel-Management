using Microsoft.Extensions.DependencyInjection;
using Main.Extensions;
using Helpers.FileOperations;

namespace UnitTests.Infrastructure.DependencyInjection
{
    [TestClass]
    public class HelperDependencyInjectionTests
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddHelpers();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void AddHelpers_ShouldRegisterFileReaderHelper()
        {
            var helper = _serviceProvider.GetService<IFileReader>();
            Assert.IsNotNull(helper);
            Assert.IsTrue(helper is FileReader);
        }
    }
}
