using Helpers.FileOperations;

namespace UnitTests.Mocks
{
    public class MockFileReader : IFileReader
    {

        private readonly string _dataDirectory;

        public MockFileReader(string dataDirectory)
        {
            _dataDirectory = dataDirectory;
        }

        public string Read(string path)
        {
            var fullPath = Path.Combine(_dataDirectory, path);
            return File.ReadAllText(fullPath);
        }
    }
}
