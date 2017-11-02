namespace UltimateTicTacToe.Model
{
    public interface IGame
    {
        bool IsGameOver { get; }
        MarkerType Winner { get; }
        IPlayer ActivePlayer { get; }
        bool PlayOneTurn(Move move);
        MarkerType GetMarkerInPosition(Move click);
        bool[,] GetActiveSubboards();
    }
}