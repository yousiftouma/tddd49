using System;

namespace UltimateTicTacToe.Model.CustomExceptions
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
