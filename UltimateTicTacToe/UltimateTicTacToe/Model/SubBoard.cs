using System;
using UltimateTicTacToe.Model.CustomExceptions;

namespace UltimateTicTacToe.Model
{
    public class SubBoard
    {
        private readonly MarkerType[,] _board;
        private readonly IRules _rules;

        public SubBoard(IRules rules)
        {
            _rules = rules;
            Winner = MarkerType.Empty;
            _board = new MarkerType[3, 3];
            IsActive = false;
            InitializeBoard();
        }

        public SubBoard(IRules rules, MarkerType[,] board, bool isActive, MarkerType winner)
        {
            _rules = rules;
            Winner = winner;
            IsActive = isActive;
            _board = board;
        }

        /// <summary>
        /// Handles placing of markers on a subboard.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="type"></param>
        /// <returns>True if allowed to be placed here</returns>
        public void PlaceMarker(Position pos, MarkerType type)
        {
            if (type == MarkerType.None)
            {
                throw new WrongMarkerTypeException("Can not place MarkerType.None on SubBoard");
            }
            try
            {
                _board[pos.X, pos.Y] = type;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"Got Exception {e}");
            }
            PossiblySetWinner(type);
        }

        public MarkerType GetMarker(Position pos)
        {
            return _board[pos.X, pos.Y];
        }

        private void PossiblySetWinner(MarkerType potentialWinner)
        {
            if (_rules.IsSubboardWon(_board, potentialWinner))
            {
                Winner = potentialWinner;
            }
            else if (_rules.IsSubboardDraw(_board))
            {
                Winner = MarkerType.None;
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _board[i, j] = MarkerType.Empty;
                }
            }
        }

        public bool HasWinner => Winner != MarkerType.Empty;

        public MarkerType Winner { get; set; }
        public bool IsActive { get; set; }
    }
}
