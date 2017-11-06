using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UltimateTicTacToe.Storage;

namespace UltimateTicTacToe.Model
{
    public class Board : IBoard
    {

        private SubBoard[,] subboards;
        private IRules _rules;
        private IDataStorageHandler _storageHandler;

        public Board(IRules rules)
        {
            _rules = rules;


            //TODO do this better
            _storageHandler = new DataStorageHandler(new FileHandler("Hej"));


            Winner = MarkerType.Empty;
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
            _storageHandler.StoreBoard(true, MarkerType.Empty, MarkerType.Cross, MarkerType.Circle, subboards);
        }

        private void UpdateActiveSubboards(Position subboardPos)
        {
            try
            {
                var chosenSubboard = subboards[subboardPos.X, subboardPos.Y];
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

        public bool[,] GetActiveSubboards()
        {
            var activeSubboards = new bool[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    activeSubboards[i, j] = subboards[i, j].IsActive;
                }
            }
            return activeSubboards;
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

        public MarkerType GetMarker(Move position)
        {
            var subboard = subboards[position.SubboardPos.X, position.SubboardPos.Y];
            return subboard.GetMarker(position.MarkerPos);
        }

        private void PossiblySetWinner(MarkerType potentialWinner)
        {
            if (_rules.IsBoardWon(subboards, potentialWinner))
            {
                Winner = potentialWinner;
            } else if (_rules.IsBoardDraw(subboards))
            {
                Winner = MarkerType.None;
            }
        }


        public bool HasWinner => Winner != MarkerType.Empty;

        public MarkerType Winner { get; set; }
    }
}
