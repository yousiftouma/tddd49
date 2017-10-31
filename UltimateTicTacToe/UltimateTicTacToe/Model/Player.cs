using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public class Player : IPlayer
    {
        private Move _move;

        public Player(MarkerType marker)
        {
            Marker = marker;
            _move = new Move();
        }

        public Move GetMove()
        {
            return _move;
        }

        public MarkerType Marker { get; }
    }
}
