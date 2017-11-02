namespace UltimateTicTacToe.Storage
{
    public interface IFileHandler
    {
        void Write(string content);
        string Read();
    }
}