using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public class WrongMarkerTypeException : Exception
    {
        public WrongMarkerTypeException()
        {

        }

        public WrongMarkerTypeException(string message) : base(message)
        {

        }

        public WrongMarkerTypeException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
