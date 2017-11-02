using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public class Player : IPlayer
    {
        public Player(MarkerType marker)
        {
            if (marker == MarkerType.None)
            {
                throw new WrongMarkerTypeException("Can not initialize Player with MarkerType.None");
            }
            Marker = marker;
        }

        public MarkerType Marker { get; }
    }
}
