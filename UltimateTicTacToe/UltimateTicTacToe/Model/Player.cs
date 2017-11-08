using UltimateTicTacToe.Model.CustomExceptions;

namespace UltimateTicTacToe.Model
{
    public class Player : IPlayer
    {
        public Player(MarkerType marker)
        {
            if (marker == MarkerType.Empty)
            {
                throw new WrongMarkerTypeException("Can not initialize Player with MarkerType.Empty");
            }
            Marker = marker;
        }

        public MarkerType Marker { get; }
    }
}
