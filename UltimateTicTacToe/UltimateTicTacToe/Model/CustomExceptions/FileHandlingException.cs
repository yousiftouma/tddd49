using System;

namespace UltimateTicTacToe.Model.CustomExceptions
{
    public class FileHandlingException : Exception
    {
        public FileHandlingException()
        {
        }

        public FileHandlingException(string message) : base(message)
        {
        }

        public FileHandlingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}