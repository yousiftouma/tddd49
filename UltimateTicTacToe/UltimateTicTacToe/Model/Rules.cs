using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public class Rules : IRules
    {
        public bool IsValidMove(SubBoard board, Position pos)
        {
            return pos.X >= 0 && pos.X <= 2 && pos.Y >= 0 && pos.Y <= 2
                && board.GetMarker(pos) == MarkerType.None
                && board.IsActive;
        }

        public bool IsBoardWon(SubBoard[,] subboards, MarkerType potentialWinner)
        {
            // Check rows
            for (int i = 0; i < 3; ++i)
            {
                if (subboards[i, 0].Winner == potentialWinner && subboards[i, 1].Winner == potentialWinner && subboards[i, 2].Winner == potentialWinner)
                {
                    return true;
                }
            }

            // Check columns
            for (int i = 0; i < 3; ++i)
            {
                if (subboards[0, i].Winner == potentialWinner && subboards[1, i].Winner == potentialWinner && subboards[2, i].Winner == potentialWinner)
                {
                    return true;
                }
            }

            // Check diagonals
            if ((subboards[0, 0].Winner == potentialWinner && subboards[1, 1].Winner == potentialWinner && subboards[2, 2].Winner == potentialWinner) ||
                (subboards[0, 2].Winner == potentialWinner && subboards[1, 1].Winner == potentialWinner && subboards[2, 0].Winner == potentialWinner))
            {
                return true;
            }
            return false;
        }
    }
}
