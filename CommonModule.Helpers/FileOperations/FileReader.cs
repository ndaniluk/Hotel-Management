namespace CommonModule.Helpers.FileOperations
{
    public static class FileReader
    {
        public static string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
}