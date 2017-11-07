using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
