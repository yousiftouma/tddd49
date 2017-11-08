using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model.CustomExceptions
{
    public class SaveStateFailException : Exception
    {
        public SaveStateFailException()
        {
        }

        public SaveStateFailException(string message) : base(message)
        {
        }

        public SaveStateFailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
