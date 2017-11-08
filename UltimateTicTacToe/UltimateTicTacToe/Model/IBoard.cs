namespace UltimateTicTacToe.Model
{
    public interface IBoard
    {
        bool HasWinner { get; }
        MarkerType Winner { get; set; }
        SubBoard[,] SubBoards { get; }
        bool PlaceMarker(Move move, MarkerType type);
        MarkerType GetMarker(Move position);
        bool[,] GetActiveSubboards();
    }
}
