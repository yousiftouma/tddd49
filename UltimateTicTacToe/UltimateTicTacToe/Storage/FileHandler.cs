using System.IO;

namespace UltimateTicTacToe.Storage
{
    public class FileHandler : IFileHandler
    {
        private readonly string _filePath;

        public FileHandler(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string content)
        {
            File.WriteAllText(_filePath, content);
        }

        public string Read()
        {
            return File.ReadAllText(_filePath);
        }

        public bool FileExists()
        {
            return File.Exists(_filePath);
        }
    }
}
