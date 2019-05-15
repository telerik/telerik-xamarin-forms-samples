using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using Telerik.XamarinForms.Map;
using Telerik.XamarinForms.ShapefileReader;
using Xamarin.Forms;
using SelectionMode = Telerik.XamarinForms.Map.SelectionMode;

namespace QSF.Examples.MapControl.HotelFloorPlanExample
{
    public class HotelFloorPlanViewModel : ExampleViewModel
    {
        private MapShapeStyleSelector shapeStyleSelector;
        private string registrationText;
        private IShape selectedRoom;
        private bool isRegisterEnabled;
        private string registrationOperationText;
        private CancellationTokenSource cancellation;

        public HotelFloorPlanViewModel()
        {
            var path = "QSF.Examples.MapControl.HotelFloorPlanExample.Data.";
            this.BaseSource = MapSource.FromResource(path + "Hotel_base.shp", typeof(HotelFloorPlanViewModel));
            this.CorridorsSource = MapSource.FromResource(path + "Hotel_Corridors.shp", typeof(HotelFloorPlanViewModel));
            this.ServiceRoomsSource = MapSource.FromResource(path + "Hotel_ServiceRooms.shp", typeof(HotelFloorPlanViewModel));
            this.LiftsAndLaddersSource = MapSource.FromResource(path + "Hotel_LiftsAndLadders.shp", typeof(HotelFloorPlanViewModel));
            this.RoomsSource = MapSource.FromResource(path + "Hotel_Rooms.shp", typeof(HotelFloorPlanViewModel));
            this.RoomsDataSource = MapSource.FromResource(path + "Hotel_Rooms.dbf", typeof(HotelFloorPlanViewModel));

            this.RoomStates = new ObservableCollection<RoomState>();
            this.RoomStates.Add(new RoomState("Reserved", Color.FromHex("#A8A9AA")));
            this.RoomStates.Add(new RoomState("Free", Color.FromHex("#46B2D6")));
            this.RoomStates.Add(new RoomState("Service Room", Color.FromHex("#7E7E7E")));

            this.RegistrationText = "Reserve";

            this.RegisterCommand = new Command(OnRegisterCommandExecuted);
            this.IsRegisterEnabled = false;

            this.ShapeStyleSelector = this.GetShapeStyleSelector();

            this.cancellation = new CancellationTokenSource();
        }

        public MapSource BaseSource { get; set; }
        public MapSource CorridorsSource { get; set; }
        public MapSource LiftsAndLaddersSource { get; set; }
        public MapSource RoomsSource { get; set; }
        public MapSource RoomsDataSource { get; set; }
        public MapSource ServiceRoomsSource { get; set; }

        public ObservableCollection<RoomState> RoomStates { get; set; }

        public Command RegisterCommand { get; set; }

        public MapShapeStyleSelector ShapeStyleSelector
        {
            get
            {
                return this.shapeStyleSelector;
            }
            set
            {
                if (this.shapeStyleSelector != value)
                {
                    this.shapeStyleSelector = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string RegistrationText
        {
            get
            {
                return this.registrationText;
            }
            set
            {
                if (this.registrationText != value)
                {
                    this.registrationText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IShape SelectedRoom
        {
            get
            {
                return this.selectedRoom;
            }
            set
            {
                if (this.selectedRoom != value)
                {
                    this.selectedRoom = value;
                    if (this.selectedRoom != null)
                    {
                        var roomState = this.selectedRoom.GetAttribute("RoomState").ToString();
                        this.RegistrationText = roomState == "Free" ? "Reserve" : "Cancel Reservation";
                        this.IsRegisterEnabled = true;
                    }
                    else
                    {
                        this.RegistrationText = "Reserve";
                        this.IsRegisterEnabled = false;
                    }

                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsRegisterEnabled
        {
            get
            {
                return this.isRegisterEnabled;
            }
            set
            {
                if (this.isRegisterEnabled != value)
                {
                    this.isRegisterEnabled = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string RegistrationOperationText
        {
            get
            {
                return this.registrationOperationText;
            }
            set
            {
                if (this.registrationOperationText != value)
                {
                    this.registrationOperationText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void OnRegisterCommandExecuted(object obj)
        {
            Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();

            if (this.selectedRoom != null)
            {
                var roomState = this.selectedRoom.GetAttribute("RoomState").ToString();
                if (roomState == "Free")
                {
                    this.selectedRoom.SetAttribute("RoomState", "Reserved");
                }
                else
                {
                    this.selectedRoom.SetAttribute("RoomState", "Free");
                }
            }

            this.ShapeStyleSelector = null;
            this.ShapeStyleSelector = this.GetShapeStyleSelector();

            string operationText;
            string roomNo = this.selectedRoom.GetAttribute("Name").ToString();
            if (this.registrationText == "Reserve")
            {
                operationText = "You have successfully reserved room No. " + roomNo;
            }
            else
            {
                operationText = "You have canceled reservation for room No. " + roomNo;
            }

            this.SelectedRoom = null;

            CancellationTokenSource cts = this.cancellation;
            this.RegistrationOperationText = operationText;
            Device.StartTimer(TimeSpan.FromSeconds(3.0), () =>
            {
                if (cts.IsCancellationRequested)
                {
                    return false;
                }

                this.RegistrationOperationText = string.Empty;
                return false;
            });
        }

        private MapShapeStyleSelector GetShapeStyleSelector()
        {
            var defaultShapeStyleSelector = new RoomStyleSelector();
            defaultShapeStyleSelector.FreeStyle = new MapShapeStyle()
            {
                FillColor = Color.FromHex("#46B2D6"),
                StrokeWidth = 0
            };

            defaultShapeStyleSelector.ReservedStyle = new MapShapeStyle()
            {
                FillColor = Color.FromHex("#A8A9AA"),
                StrokeWidth = 0
            };

            return defaultShapeStyleSelector;
        }
    }
}
