using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTicTacToe.Storage;

namespace UltimateTicTacToe.Model
{

    public class Game : IGame
    {
        private readonly IBoard _gameBoard;
        private readonly IPlayer _playerOne;
        private readonly IPlayer _playerTwo;
        private readonly IDataStorageHandler _storageHandler;
        private bool _playerOnesTurn;

        public Game(IBoard board, IPlayer playerOne, IPlayer playerTwo, bool playerOnesTurn, IDataStorageHandler storageHandler)
        {
            _gameBoard = board;
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _storageHandler = storageHandler;
            _playerOnesTurn = playerOnesTurn;
        }

        public MarkerType GetMarkerInPosition(Move click)
        {
            return _gameBoard.GetMarker(click);
        }

        public bool[,] GetActiveSubboards()
        {
            return _gameBoard.GetActiveSubboards();
        }

        public bool PlayOneTurn(Move move)
        {
            if (IsGameOver)
            {
                return false;
            }
            try
            {
                var turnSuccessful = _gameBoard.PlaceMarker(move.SubboardPos, move.MarkerPos,
                    _playerOnesTurn ? _playerOne.Marker : _playerTwo.Marker);

                if (turnSuccessful)
                {
                    _playerOnesTurn = !_playerOnesTurn;
                }
                SaveGameState();
                return turnSuccessful;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Got exception {e}");
                return false;
            }
        }

        private void SaveGameState()
        {
            _storageHandler.StoreBoard(_playerOnesTurn, Winner, _playerOne.Marker, _playerTwo.Marker, _gameBoard.SubBoards);
        }

        public bool IsGameOver => _gameBoard.HasWinner;
        public MarkerType Winner => _gameBoard.Winner;
        public IPlayer ActivePlayer => _playerOnesTurn ? _playerOne : _playerTwo;
    }
}
