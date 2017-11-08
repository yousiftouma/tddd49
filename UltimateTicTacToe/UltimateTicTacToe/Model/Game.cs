using System;
using UltimateTicTacToe.Model.CustomExceptions;
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


        /// <summary>
        /// Tries to get the marker in position click.
        /// </summary>
        /// <param name="click"></param>
        /// <returns></returns>
        public MarkerType GetMarkerInPosition(Move click)
        {
            try
            {
                return _gameBoard.GetMarker(click);
            }
            catch (BoardOutOfRangeException e)
            {
                throw new MoveFailedException("Failed to get marker in position " +
                                              $"({click.MarkerPos.X}, {click.MarkerPos.Y}) in SubBoard " +
                                              $"({click.SubboardPos.X}, {click.SubboardPos.Y}). " +
                                              "See inner exception for details", e);
            }
        }

        public bool[,] GetActiveSubboards()
        {
            return _gameBoard.GetActiveSubboards();
        }


        /// <summary>
        /// Tries to progress the game using move, returning whether game successfully progressed.
        /// </summary>
        /// <param name="move"></param>
        /// <returns>True if move was performed.</returns>
        public bool PlayOneTurn(Move move)
        {
            if (IsGameOver)
            {
                return false;
            }
            bool turnSuccessful;
            try
            {
                turnSuccessful = _gameBoard.PlaceMarker(move,
                    _playerOnesTurn ? _playerOne.Marker : _playerTwo.Marker);
            }
            catch (Exception e)
            {
                var turn = _playerOnesTurn ? "Player One" : "Player Two";
                throw new MoveFailedException("Failed to perform move in position " +
                                              $"({move.MarkerPos.X}, {move.MarkerPos.Y}) in SubBoard " +
                                              $"({move.SubboardPos.X}, {move.SubboardPos.Y}) when it was {turn}'s turn. " +
                                              "See inner exception for details", e);
            }

            if (!turnSuccessful) return false;

            _playerOnesTurn = !_playerOnesTurn;
            try
            {
                SaveGameState();
            }
            catch (Exception e)
            {
                throw new SaveStateFailException("Failed to save state, see inner exception for details", e);
            }
            return true;
        }

        private void SaveGameState()
        {
            var boardDto = new BoardDto(_playerOnesTurn, Winner, _playerOne.Marker, _playerTwo.Marker,
                _gameBoard.SubBoards);
            _storageHandler.StoreBoard(boardDto);
        }

        public bool IsGameOver => _gameBoard.HasWinner;
        public MarkerType Winner => _gameBoard.Winner;
        public IPlayer ActivePlayer => _playerOnesTurn ? _playerOne : _playerTwo;
    }
}
