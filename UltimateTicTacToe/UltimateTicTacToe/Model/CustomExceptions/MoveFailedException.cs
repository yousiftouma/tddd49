using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model.CustomExceptions
{
    public class MoveFailedException : Exception
    {
        public MoveFailedException()
        {
        }

        public MoveFailedException(string message) : base(message)
        {
        }

        public MoveFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
