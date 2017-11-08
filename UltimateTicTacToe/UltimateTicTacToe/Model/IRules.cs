namespace UltimateTicTacToe.Model
{
    public interface IRules
    {
        bool IsValidMove(SubBoard board, Position pos);
        bool IsBoardFinished(SubBoard[,] subboards, MarkerType potentialWinner, out MarkerType winner);
        bool IsSubBoardFinished(MarkerType[,] board, MarkerType potentialWinner, out MarkerType winner);
    }
}