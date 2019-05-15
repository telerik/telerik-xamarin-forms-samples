using Telerik.XamarinForms.Map;
using Telerik.XamarinForms.ShapefileReader;
using Xamarin.Forms;

namespace QSF.Examples.MapControl.HotelFloorPlanExample
{
    public class RoomStyleSelector : MapShapeStyleSelector
    {
        public MapShapeStyle FreeStyle { get; set; }
        public MapShapeStyle ReservedStyle { get; set; }

        public override MapShapeStyle SelectStyle(object shape, BindableObject container)
        {
            var state = shape as IShape;
            if (state != null)
            {
                var roomState = state.GetAttribute("RoomState").ToString();
                if (!string.IsNullOrEmpty(roomState))
                {
                    if (roomState == "Occupied" || roomState == "Reserved")
                    {
                        return this.ReservedStyle;
                    }

                    return this.FreeStyle;
                }
            }

            return base.SelectStyle(shape, container);
        }
    }
}