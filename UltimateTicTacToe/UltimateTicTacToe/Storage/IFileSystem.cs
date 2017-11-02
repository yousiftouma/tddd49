using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Storage
{
    public interface IFileSystem
    {
        bool FileExists(string path);
        void CreateFile(string path);
    }
}
