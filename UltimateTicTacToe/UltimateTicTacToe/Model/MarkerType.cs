namespace UltimateTicTacToe.Model
{
    public enum MarkerType
    {
        Empty = 0,
        Cross = 1,
        Circle = 2,
        None = -1
    };

    public static class MarkerTypeExtensions
    {
        public static string MarkerTypeToString(this MarkerType marker)
        {
            switch (marker)
            {
                case MarkerType.Empty:
                    return "";
                case MarkerType.Cross:
                    return "X";
                case MarkerType.Circle:
                    return "O";
                default:
                    return "";
            }
        }
    }
}
