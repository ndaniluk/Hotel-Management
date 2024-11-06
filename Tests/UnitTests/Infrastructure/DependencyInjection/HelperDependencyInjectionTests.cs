using Microsoft.Extensions.DependencyInjection;
using Main.Extensions;
using Helpers.FileOperations;

namespace UnitTests.Infrastructure.DependencyInjection
{
    [TestClass]
    public class HelperDependencyInjectionTests
    {
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHelpers();
            _serviceProvider = serviceCollection.BuildServiceProvider();
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
