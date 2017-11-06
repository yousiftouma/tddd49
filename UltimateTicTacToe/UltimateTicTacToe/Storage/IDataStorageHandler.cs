using UltimateTicTacToe.Model;

namespace UltimateTicTacToe.Storage
{
    public interface IDataStorageHandler
    {
        void StoreBoard(bool isPlayerOneTurn, MarkerType winner, MarkerType playerOne, 
            MarkerType playerTwo, SubBoard[,] subboards);

        bool LoadBoard(IRules rules, out BoardDto boardDto);
    }
}