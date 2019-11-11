using Xamarin.Forms;

namespace QSF.Examples.MapControl.HotelFloorPlanExample
{
    public class RoomState
    {
        public RoomState(string state, Color stateColor)
        {
            this.State = state;
            this.StateColor = stateColor;
        }

        public string State { get; set; }
        public Color StateColor { get; set; }
    }
}