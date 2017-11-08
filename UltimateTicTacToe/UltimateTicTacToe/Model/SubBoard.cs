using System;
using UltimateTicTacToe.Model.CustomExceptions;

namespace UltimateTicTacToe.Model
{
    public class SubBoard
    {
        private const int SubBoardSize = 3;
        private readonly MarkerType[,] _board;
        private readonly IRules _rules;

        /// <summary>
        /// Initializes an empty SubBoard that will use the given <paramref name="rules"/>.
        /// </summary>
        /// <param name="rules">The rules that this SubBoard will adhere to.</param>
        public SubBoard(IRules rules)
        {
            _rules = rules;
            Winner = MarkerType.Empty;
            _board = new MarkerType[SubBoardSize, SubBoardSize];
            IsActive = false;
            InitializeEmptyBoard();
        }

        /// <summary>
        /// Initializes a SubBoard from the state given by its parameters.
        /// </summary>
        /// <param name="rules"></param>
        /// <param name="board"></param>
        /// <param name="isActive"></param>
        /// <param name="winner"></param>
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
                throw new BoardOutOfRangeException($"Can not place marker on SubBoard position ({pos.X}, {pos.Y}).", e);
            }
            PossiblySetWinner(type);
        }

        public MarkerType GetMarker(Position pos)
        {
            try
            {
                return _board[pos.X, pos.Y];
            }
            catch (IndexOutOfRangeException e)
            {
                throw new BoardOutOfRangeException($"Position ({pos.X}, {pos.Y}) outside of SubBoard.", e);
            }
        }

        private void PossiblySetWinner(MarkerType potentialWinner)
        {
            MarkerType w;
            if (_rules.IsSubBoardFinished(_board, potentialWinner, out w))
            {
                Winner = w;
            }
        }

        private void InitializeEmptyBoard()
        {
            for (var i = 0; i < SubBoardSize; i++)
            {
                for (var j = 0; j < SubBoardSize; j++)
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
