using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public class Board : IBoard
    {

        private SubBoard[,] subboards;
        private IRules _rules;


        public Board(IRules rules)
        {
            _rules = rules;
            Winner = MarkerType.None;
            subboards = new SubBoard[3, 3];
            InitializeBoards();
        }

        private void InitializeBoards()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    subboards[i, j] = new SubBoard(_rules) { IsActive = true };
                }
            }
        }

        private void UpdateActiveSubboards(Position subboardPos)
        {
            try
            {
                var chosenSubboard = subboards[subboardPos.X, subboardPos.Y];
                if (!chosenSubboard.HasWinner)
                {
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

        private void SetSubboardsActivity(bool activeState)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!subboards[i, j].HasWinner)
                    {
                        subboards[i, j].IsActive = activeState;
                    }
                    else
                    {
                        subboards[i, j].IsActive = false;
                    }
                }
            }
        }


        public bool PlaceMarker(Position subboardPos, Position markerPos, MarkerType type)
        {
            try
            {
                var subboard = subboards[subboardPos.X, subboardPos.Y];
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

        public SubBoard GetSubboard(Position subboardPos)
        {
            return subboards[subboardPos.X, subboardPos.Y];
        }

        private void PossiblySetWinner(MarkerType potentialWinner)
        {
            if (_rules.IsBoardWon(subboards, potentialWinner))
            {
                Winner = potentialWinner;
            }
        }


        public bool HasWinner => Winner != MarkerType.None;

        public MarkerType Winner { get; set; }
    }
}
