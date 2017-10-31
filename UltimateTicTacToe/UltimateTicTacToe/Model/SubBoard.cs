using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{

    public class SubBoard
    {
        private MarkerType[,] board;


        public SubBoard()
        {
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
        public bool PlaceMarker(Position pos, MarkerType type)
        {
            if (board[pos.X, pos.Y] == MarkerType.None)
            {
                board[pos.X, pos.Y] = type;
                PossiblySetWinner(type);
                return true;
            }
            return false;
        }

        public MarkerType GetMarker(Position pos)
        {
            return board[pos.X, pos.Y];
        }

        private void PossiblySetWinner(MarkerType potentialWinner)
        {

            for (int i = 0; i < 3; ++i)
            {
                if (board[i, 0] == potentialWinner && board[i, 1] == potentialWinner && board[i, 2] == potentialWinner)
                {
                    Winner = potentialWinner;
                    return;
                }
            }

            for (int i = 0; i < 3; ++i)
            {
                if (board[0, i] == potentialWinner && board[1, i] == potentialWinner && board[2, i] == potentialWinner)
                {
                    Winner = potentialWinner;
                    return;
                }
            }

            if (board[0, 0] == potentialWinner && board[1, 1] == potentialWinner && board[2, 2] == potentialWinner)
            {
                Winner = potentialWinner;
            }
            else if (board[0, 2] == potentialWinner && board[1, 1] == potentialWinner && board[2, 0] == potentialWinner)
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
