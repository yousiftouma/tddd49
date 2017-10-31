using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public class Move
    {
        /// <summary>
        /// Represents the Position of the subboard on the larger board
        /// </summary>
        public Position SubboardPos { get; set; } = null;

        /// <summary>
        /// Represents the Position of the marker on the subboard
        /// </summary>
        public Position MarkerPos { get; set; } = null;

    }
}
