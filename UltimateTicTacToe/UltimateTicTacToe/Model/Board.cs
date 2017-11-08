using System;
using UltimateTicTacToe.Model.CustomExceptions;

namespace UltimateTicTacToe.Model
{
    public class Board : IBoard
    {
        private const int BoardSize = 3;
        private readonly IRules _rules;

        /// <summary>
        /// Initializes an empty Board using the given <paramref name="rules"/>.
        /// </summary>
        /// <param name="rules">The rules that the board will adhere to.</param>
        public Board(IRules rules)
        {
            _rules = rules;
            Winner = MarkerType.Empty;
            SubBoards = new SubBoard[BoardSize, BoardSize];
            InitializeSubBoards();
        }

        /// <summary>
        /// Initializes a board with the state given by its parameters.
        /// </summary>
        /// <param name="rules">The rules that the board will adhere to.</param>
        /// <param name="winner">The winner of the board.</param>
        /// <param name="subboards">Two dimensional array of SubBoards.</param>
        public Board(IRules rules, MarkerType winner, SubBoard[,] subboards)
        {
            _rules = rules;
            Winner = winner;
            SubBoards = subboards;
        }

        private void InitializeSubBoards()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    SubBoards[i, j] = new SubBoard(_rules) { IsActive = true };
                }
            }
        }

        private void UpdateActiveSubboards(Position subboardPos)
        {
            try
            {
                var chosenSubboard = SubBoards[subboardPos.X, subboardPos.Y];
                if (!chosenSubboard.HasWinner)
                {
                    // Activate only the chosen subboard
                    SetSubboardsActivity(false);
                    chosenSubboard.IsActive = true;
                    return;
                }
                SetSubboardsActivity(true);
            }
            catch (IndexOutOfRangeException e)
            {
                throw new BoardOutOfRangeException($"SubBoardPosition ({subboardPos.X}, {subboardPos.Y}) is outside of the Board.", e);
            }
        }

        /// <summary>
        /// Sets all subboards to <paramref name="activeState"/> unless they already have a winner in which case they are always set to inactive.
        /// </summary>
        /// <param name="activeState"></param>
        private void SetSubboardsActivity(bool activeState)
        {
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    SubBoards[i, j].IsActive = !SubBoards[i, j].HasWinner && activeState;
                }
            }
        }

        /// <summary>
        /// Returns a two dimensional array of bools where each position states whether the SubBoard at the corresponding position is active or not.
        /// </summary>
        /// <returns></returns>
        public bool[,] GetActiveSubboards()
        {
            var activeSubboards = new bool[BoardSize, BoardSize];
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    activeSubboards[i, j] = SubBoards[i, j].IsActive;
                }
            }
            return activeSubboards;
        }


        /// <summary>
        /// Handles placing of markers on the board.
        /// </summary>
        /// <param name="move">Where to place the marker.</param>
        /// <param name="type">Which MarkerType to place.</param>
        /// <returns>If we succeeded placing the marker.</returns>
        public bool PlaceMarker(Move move, MarkerType type)
        {
            SubBoard subBoardToPlaceOn;
            var subboardPos = move.SubboardPos;
            var markerPos = move.MarkerPos;

            try
            {
                subBoardToPlaceOn = SubBoards[subboardPos.X, subboardPos.Y];
            }
            catch (IndexOutOfRangeException e)
            {
                throw new BoardOutOfRangeException($"SubBoardPosition ({subboardPos.X}, {subboardPos.Y}) is outside of the Board.", e);
            }

            if (_rules.IsValidMove(subBoardToPlaceOn, markerPos))
            {
                try
                {
                    subBoardToPlaceOn.PlaceMarker(markerPos, type);
                    UpdateActiveSubboards(markerPos);
                    PossiblySetWinner(type);
                }
                catch (Exception e)
                {
                    throw new MoveFailedException("Failed to place marker, see inner exception for details.", e);
                }
                return true;
            }
            return false;

        }

        /// <summary>
        /// Get marker in position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public MarkerType GetMarker(Move position)
        {
            SubBoard subboard;
            var subboardPos = position.SubboardPos;
            var markerPos = position.MarkerPos;
            try
            {
                subboard = SubBoards[position.SubboardPos.X, position.SubboardPos.Y];
            }
            catch (IndexOutOfRangeException e)
            {
                throw new BoardOutOfRangeException($"SubBoardPosition ({subboardPos.X}, {subboardPos.Y}) is outside of the Board.", e);
            }
            try
            {
                return subboard.GetMarker(markerPos);
            }
            catch (BoardOutOfRangeException e)
            {
                throw new BoardOutOfRangeException($"Failed to get marker using MarkerPos ({markerPos.X}, {markerPos.Y}), see inner exception.", e);
            }
        }

        /// <summary>
        /// Checks if board has a winner or draw and sets property Winner accordingly.
        /// </summary>
        /// <param name="potentialWinner">The MarkerType that we want to check if it has won.</param>
        private void PossiblySetWinner(MarkerType potentialWinner)
        {
            MarkerType w;
            if (!_rules.IsBoardFinished(SubBoards, potentialWinner, out w) || (w == MarkerType.Empty)) return;
            Winner = w;
            SetSubboardsActivity(false);
        }

        public SubBoard[,] SubBoards { get; }

        public bool HasWinner => Winner != MarkerType.Empty;

        public MarkerType Winner { get; set; }
    }
}
