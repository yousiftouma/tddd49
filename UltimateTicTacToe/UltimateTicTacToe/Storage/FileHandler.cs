using System;
using System.IO;
using UltimateTicTacToe.Model.CustomExceptions;

namespace UltimateTicTacToe.Storage
{
    public class FileHandler : IFileHandler
    {
        private readonly string _filePath;

        /// <summary>
        /// Handles operations regarding file with path <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">Path of file to keep track of.</param>
        public FileHandler(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Writes <paramref name="content"/> to the file.
        /// </summary>
        /// <param name="content">Content to write to file.</param>
        public void Write(string content)
        {
            try
            {
                File.WriteAllText(_filePath, content);
            }
            catch (Exception e)
            {
                throw new FileHandlingException("Failed to write to file, see inner exception for details.", e);
            }
        }

        /// <summary>
        /// Reads all the text in the file.
        /// </summary>
        /// <returns>The content of the file.</returns>
        public string Read()
        {
            try
            {
                return File.ReadAllText(_filePath);
            }
            catch (Exception e)
            {
                throw new FileHandlingException("Failed to read from file, see inner exception for details.", e);
            }
        }

        /// <summary>
        /// Checks if the file to keep track of exists.
        /// </summary>
        /// <returns></returns>
        public bool FileExists()
        {
            return File.Exists(_filePath);
        }
    }
}
