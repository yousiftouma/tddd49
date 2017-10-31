using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{

    public class Game
    {
        private IBoard _gameBoard;
        private IPlayer _playerOne, _playerTwo;
        private bool _playerOnesTurn;

        public Game(IBoard board, IPlayer playerOne, IPlayer playerTwo)
        {
            _gameBoard = board;
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _playerOnesTurn = true;
        }

        public bool PlayOneTurn(Move move)
        {
            try
            {

            var turnSuccessful = _gameBoard.PlaceMarker(move.SubboardPos, move.MarkerPos,
                _playerOnesTurn ? _playerOne.Marker : _playerTwo.Marker);

            if (turnSuccessful)
            {
                _playerOnesTurn = !_playerOnesTurn;
            }
            return turnSuccessful;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Got exception {e}");
                return false;
            }
        }

        public bool IsGameOver => _gameBoard.HasWinner;
    }
}
