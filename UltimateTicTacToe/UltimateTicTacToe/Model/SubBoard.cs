using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{

    public class SubBoard
    {
        //TODO possibly make this class only available to Board
        private MarkerType[,] board;
        private IRules _rules;


        public SubBoard(IRules rules)
        {
            _rules = rules;
            Winner = MarkerType.None;
            board = new MarkerType[3, 3];
            IsActive = false;
            InitializeBoard();
        }

        /// <summary>
        /// Handles placing of markers on a subboard.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="type"></param>
        /// <returns>True if allowed to be placed here</returns>
        public void PlaceMarker(Position pos, MarkerType type)
        { 
            try
            {
                board[pos.X, pos.Y] = type;
                PossiblySetWinner(type);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"Got exception {e}");
            }
        }

        public MarkerType GetMarker(Position pos)
        {
            return board[pos.X, pos.Y];
        }

        private void PossiblySetWinner(MarkerType potentialWinner)
        {
            if (_rules.IsSubboardWon(board, potentialWinner))
            {
                Winner = potentialWinner;
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = MarkerType.None;
                }
            }
        }

        public bool HasWinner => Winner != MarkerType.None;

        public MarkerType Winner { get; set; }
        public bool IsActive { get; set; }
    }
}
