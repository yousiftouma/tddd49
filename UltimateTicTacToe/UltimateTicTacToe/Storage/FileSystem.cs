using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Storage
{
    public class FileSystem : IFileSystem
    {
        private readonly FileSystem _fileSystem;

        public FileSystem(FileSystem fileSystem)
        {
            this._fileSystem = fileSystem;
        }

        public bool FileExists(string path)
        {
            return _fileSystem.FileExists(path);
        }

        public void CreateFile(string path)
        {
            _fileSystem.CreateFile(path);
        }
    }
}
