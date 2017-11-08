namespace UltimateTicTacToe.Model
{
    public class Rules : IRules
    {
        /// <summary>
        /// Checks whether it is ok to perform move on Position <paramref name="pos"/> on SubBoard <paramref name="board"/>.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsValidMove(SubBoard board, Position pos)
        {
            return pos.X >= 0 
                && pos.Y >= 0 
                && pos.X <= 2 
                && pos.Y <= 2
                && board.GetMarker(pos) == MarkerType.Empty
                && board.IsActive;
        }

        /// <summary>
        /// Checks if <paramref name="potentialWinner"/> has won the board by checking the underlying <paramref name="subboards"/>.
        /// </summary>
        /// <param name="subboards"></param>
        /// <param name="potentialWinner">The MarkerType that might have won.</param>
        /// <param name="winner">Contains the winner if there is one</param>
        /// <returns>True if the board is won or draw.</returns>
        public bool IsBoardFinished(SubBoard[,] subboards, MarkerType potentialWinner, out MarkerType winner)
        {
            // Check rows and columns
            for (var i = 0; i < 3; ++i)
            {
                if ((subboards[i, 0].Winner == potentialWinner && subboards[i, 1].Winner == potentialWinner && subboards[i, 2].Winner == potentialWinner) ||
                    (subboards[0, i].Winner == potentialWinner && subboards[1, i].Winner == potentialWinner && subboards[2, i].Winner == potentialWinner))
                {
                    winner = potentialWinner;
                    return true;
                }
            }

            // Check diagonals
            if ((subboards[0, 0].Winner == potentialWinner && subboards[1, 1].Winner == potentialWinner && subboards[2, 2].Winner == potentialWinner) ||
                (subboards[0, 2].Winner == potentialWinner && subboards[1, 1].Winner == potentialWinner && subboards[2, 0].Winner == potentialWinner))
            {
                winner = potentialWinner;
                return true;
            }

            // Check for draw
            // It's a draw if all SubBoards have a "winner" (this can include None which indicates draw in that SubBoard)
            // since we have already asserted that there is no "real" winner, this must mean it is a draw in the Board
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (!subboards[i, j].HasWinner)
                    {
                        // Can't be draw since some SubBoard has yet to be decided to be won or draw
                        winner = MarkerType.Empty;
                        return false;
                    }
                }
            }

            // It is a draw
            winner = MarkerType.None;
            return true;
        }

        /// <summary>
        /// Checks if a SubBoard is won by checking if the potentialWinner has three markers in a row.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="potentialWinner"></param>
        /// <param name="winner"></param>
        /// <returns></returns>
        public bool IsSubBoardFinished(MarkerType[,] board, MarkerType potentialWinner, out MarkerType winner)
        {
            // Check rows and columns
            for (var i = 0; i < 3; ++i)
            {
                if ((board[i, 0] == potentialWinner && board[i, 1] == potentialWinner && board[i, 2] == potentialWinner) ||
                    (board[0, i] == potentialWinner && board[1, i] == potentialWinner && board[2, i] == potentialWinner))
                {
                    winner = potentialWinner;
                    return true;
                }
            }

            // Check diagonals
            if ((board[0, 0] == potentialWinner && board[1, 1] == potentialWinner && board[2, 2] == potentialWinner) ||
                (board[0, 2] == potentialWinner && board[1, 1] == potentialWinner && board[2, 0] == potentialWinner))
            {
                winner = potentialWinner;
                return true;
            }

            // Check for draw
            // It's a draw if all positions are occupied since we have already asserted that there is no "real" winner
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (board[i, j] == MarkerType.Empty)
                    {
                        // It can't be draw since some position is unoccupied
                        winner = MarkerType.Empty;
                        return false;
                    }
                }
            }

            // It is a draw
            winner = MarkerType.None;
            return true;
        }
    }
}
