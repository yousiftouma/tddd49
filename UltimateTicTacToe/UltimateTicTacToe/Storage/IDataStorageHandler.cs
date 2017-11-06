using UltimateTicTacToe.Model;

namespace UltimateTicTacToe.Storage
{
    public interface IDataStorageHandler
    {
        void StoreBoard(bool isPlayerOneTurn, MarkerType winner, MarkerType playerOne, 
            MarkerType playerTwo, SubBoard[,] subboards);
    }
}