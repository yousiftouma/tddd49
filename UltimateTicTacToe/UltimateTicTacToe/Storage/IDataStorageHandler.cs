using UltimateTicTacToe.Model;

namespace UltimateTicTacToe.Storage
{
    public class BoardDto
    {
        public bool IsPlayerOneTurn { get; set; }
        public MarkerType Winner { get; set; }
        public MarkerType PlayerOne { get; set; }
        public MarkerType PlayerTwo { get; set; }
        public SubBoard[,] Subboards { get; set; }

        public BoardDto(bool isPlayerOneTurn, MarkerType winner, MarkerType playerOne, MarkerType playerTwo, SubBoard[,] subboards)
        {
            IsPlayerOneTurn = isPlayerOneTurn;
            Winner = winner;
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            Subboards = subboards;
        }

        public BoardDto() { }
    }
    public interface IDataStorageHandler
    {
        void StoreBoard(BoardDto boardDto);

        bool LoadBoard(IRules rules, out BoardDto boardDto);
    }
}