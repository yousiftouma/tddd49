using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Storage
{
    public class DataStorageHandler
    {
        private IFileHandler _fileHandler;

        public DataStorageHandler(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

    }
}
