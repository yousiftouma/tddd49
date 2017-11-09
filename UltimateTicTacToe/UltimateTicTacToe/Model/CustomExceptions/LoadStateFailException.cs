using System;

namespace UltimateTicTacToe.Model.CustomExceptions
{
    public class LoadStateFailException : Exception
    {
        public LoadStateFailException()
        {
        }

        public LoadStateFailException(string message) : base(message)
        {
        }

        public LoadStateFailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}