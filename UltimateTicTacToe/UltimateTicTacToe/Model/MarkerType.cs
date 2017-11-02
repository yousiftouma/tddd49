using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Model
{
    public enum MarkerType
    {
        None,
        Cross,
        Circle
    };

    public static class MarkerTypeExtensions
    {
        public static string MarkerTypeToString(this MarkerType marker)
        {
            switch (marker)
            {
                case MarkerType.None:
                    return "-";
                case MarkerType.Cross:
                    return "X";
                case MarkerType.Circle:
                    return "O";
                default:
                    return "-";
            }
        }
    }
}
