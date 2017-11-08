using System;

namespace UltimateTicTacToe.Model
{
    public class Board : IBoard
    {
        private readonly IRules _rules;

        public Board(IRules rules)
        {
            _rules = rules;
            Winner = MarkerType.Empty;
            SubBoards = new SubBoard[3, 3];
            InitializeBoards();
        }

        public Board(IRules rules, MarkerType winner, SubBoard[,] subboards)
        {
            _rules = rules;
            Winner = winner;
            SubBoards = subboards;
        }

        private void InitializeBoards()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
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
                Console.WriteLine($"Got exception {e}");
            }
        }

        /// <summary>
        /// Sets all subboards to <paramref name="activeState"/> unless they already have a winner in which case they are always set to inactive.
        /// </summary>
        /// <param name="activeState"></param>
        private void SetSubboardsActivity(bool activeState)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    SubBoards[i, j].IsActive = !SubBoards[i, j].HasWinner && activeState;
                }
            }
        }

        public bool[,] GetActiveSubboards()
        {
            var activeSubboards = new bool[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    activeSubboards[i, j] = SubBoards[i, j].IsActive;
                }
            }
            return activeSubboards;
        }


        public bool PlaceMarker(Position subboardPos, Position markerPos, MarkerType type)
        {
            try
            {
                var subboard = SubBoards[subboardPos.X, subboardPos.Y];
                if (_rules.IsValidMove(subboard, markerPos))
                {
                    subboard.PlaceMarker(markerPos, type);
                    UpdateActiveSubboards(markerPos);
                    PossiblySetWinner(type);
                    return true;
                }
                return false;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"Got exception {e}");
                return false;
            }
        }

        public MarkerType GetMarker(Move position)
        {
            var subboard = SubBoards[position.SubboardPos.X, position.SubboardPos.Y];
            return subboard.GetMarker(position.MarkerPos);
        }

        private void PossiblySetWinner(MarkerType potentialWinner)
        {
            if (_rules.IsBoardWon(SubBoards, potentialWinner))
            {
                Winner = potentialWinner;
                SetSubboardsActivity(false);
            }
            else if (_rules.IsBoardDraw(SubBoards))
            {
                Winner = MarkerType.None;
                SetSubboardsActivity(false);
            }
        }

        public SubBoard[,] SubBoards { get; }

        public bool HasWinner => Winner != MarkerType.Empty;

        public MarkerType Winner { get; set; }
    }
}
