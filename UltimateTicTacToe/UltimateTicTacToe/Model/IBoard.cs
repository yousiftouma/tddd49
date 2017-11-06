using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public interface IBoard
    {
        bool HasWinner { get; }
        MarkerType Winner { get; set; }
        SubBoard[,] SubBoards { get; }
        bool PlaceMarker(Position subboardPos, Position markerPos, MarkerType type);
        MarkerType GetMarker(Move position);
        bool[,] GetActiveSubboards();
    }
}
