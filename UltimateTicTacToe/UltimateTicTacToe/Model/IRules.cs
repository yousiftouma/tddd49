namespace UltimateTicTacToe.Model
{
    public interface IRules
    {
        bool IsValidMove(SubBoard board, Position pos);
        bool IsBoardWon(SubBoard[,] subboards, MarkerType potentialWinner);
        bool IsSubboardWon(MarkerType[,] board, MarkerType potentialWinner);
        bool IsBoardDraw(SubBoard[,] subboards);
        bool IsSubboardDraw(MarkerType[,] board);
    }
}