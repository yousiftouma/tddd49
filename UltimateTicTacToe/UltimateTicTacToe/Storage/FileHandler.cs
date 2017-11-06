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
        public string FilePath { get; }

        public FileHandler(string filePath)
        {
            FilePath = filePath;
        }

        public void Write(string content)
        {
            File.WriteAllText(FilePath, content);
        }

        public string Read()
        {
            return File.ReadAllText(FilePath);
        }
    }
}
