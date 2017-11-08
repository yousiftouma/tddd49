using System;

namespace UltimateTicTacToe.Model.CustomExceptions
{
    public class BoardOutOfRangeException : Exception
    {
        public BoardOutOfRangeException()
        {
        }

        public BoardOutOfRangeException(string message) : base(message)
        {
        }

        public BoardOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}