namespace UltimateTicTacToe.Model
{
    public interface IGame
    {
        bool IsGameOver { get; }
        MarkerType Winner { get; }

        bool PlayOneTurn(Move move);
    }
}