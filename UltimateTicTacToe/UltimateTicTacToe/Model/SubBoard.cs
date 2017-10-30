using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model {

    public class SubBoard {

        private MarkerType winner = MarkerType.None;

        MarkerType[,] board = new MarkerType[3, 3];

        public void PlaceMarker(Position pos, MarkerType type) {
            board[pos.X, pos.Y] = type;
            PossiblySetWinner(type);
        }

        public MarkerType GetMarker(Position pos) {
            return board[pos.X, pos.Y];
        }

        private void PossiblySetWinner(MarkerType potentialWinner) {

            for (int i = 0; i < 3; ++i) {
                if (board[i, 0] == potentialWinner && board[i, 1] == potentialWinner && board[i, 2] == potentialWinner) {
                    winner = potentialWinner;
                    return;
                }
            }

            for (int i = 0; i < 3; ++i) {
                if (board[0, i] == potentialWinner && board[1, i] == potentialWinner && board[2, i] == potentialWinner) {
                    winner = potentialWinner;
                    return;
                }
            }

            if (board[0, 0] == potentialWinner && board[1, 1] == potentialWinner && board[2, 2] == potentialWinner) {
                winner = potentialWinner;
            } else if (board[0, 2] == potentialWinner && board[1, 1] == potentialWinner && board[2, 0] == potentialWinner) {
                winner = potentialWinner;
            }

        }

        public bool HasWinner {
            get
            {
                return winner != MarkerType.None;
            } }

        public MarkerType Winner {
            get
            {
                return winner;
            }

            set
            {
                winner = value;
            } }

    }
}
