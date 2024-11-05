namespace Helpers.FileOperations
{
    public class FileReader : IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
