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


        public Board()
        {
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
                    subboards[i, j] = new SubBoard { IsActive = true };
                }
            }
        }

        private void UpdateActiveSubboards(Position subboardPos)
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
                }
            }
        }


        public bool PlaceMarker(Position subboardPos, Position markerPos, MarkerType type)
        {
            var subboard = subboards[subboardPos.X, subboardPos.Y];
            if (subboard.IsActive && subboard.GetMarker(markerPos) == MarkerType.None)
            {
                subboard.PlaceMarker(markerPos, type);
                UpdateActiveSubboards(markerPos);
                PossiblySetWinner(type);
                return true;
            }
            return false;
        }

        private void PossiblySetWinner(MarkerType potentialWinner)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (subboards[i, 0].Winner == potentialWinner && subboards[i, 1].Winner == potentialWinner && subboards[i, 2].Winner == potentialWinner)
                {
                    Winner = potentialWinner;
                    return;
                }
            }

            for (int i = 0; i < 3; ++i)
            {
                if (subboards[0, i].Winner == potentialWinner && subboards[1, i].Winner == potentialWinner && subboards[2, i].Winner == potentialWinner)
                {
                    Winner = potentialWinner;
                    return;
                }
            }

            if (subboards[0, 0].Winner == potentialWinner && subboards[1, 1].Winner == potentialWinner && subboards[2, 2].Winner == potentialWinner)
            {
                Winner = potentialWinner;
            }
            else if (subboards[0, 2].Winner == potentialWinner && subboards[1, 1].Winner == potentialWinner && subboards[2, 0].Winner == potentialWinner)
            {
                Winner = potentialWinner;
            }
        }


        public bool HasWinner => Winner != MarkerType.None;

        public MarkerType Winner { get; set; }
    }
}
