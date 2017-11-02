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
                && board.GetMarker(pos) == MarkerType.Empty
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

        public bool IsBoardDraw(SubBoard[,] subboards)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!subboards[i, j].HasWinner)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsSubboardWon(MarkerType[,] board, MarkerType potentialWinner)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (board[i, 0] == potentialWinner && board[i, 1] == potentialWinner && board[i, 2] == potentialWinner)
                {
                    return true;
                }
            }

            for (int i = 0; i < 3; ++i)
            {
                if (board[0, i] == potentialWinner && board[1, i] == potentialWinner && board[2, i] == potentialWinner)
                {
                    return true;
                }
            }

            if ((board[0, 0] == potentialWinner && board[1, 1] == potentialWinner && board[2, 2] == potentialWinner) ||
                (board[0, 2] == potentialWinner && board[1, 1] == potentialWinner && board[2, 0] == potentialWinner))
            {
                return true;
            }
                return false;
        }

        public bool IsSubboardDraw(MarkerType[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == MarkerType.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
