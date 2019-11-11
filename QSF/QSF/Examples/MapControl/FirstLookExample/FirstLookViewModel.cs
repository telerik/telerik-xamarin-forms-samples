using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using Telerik.XamarinForms.ShapefileReader;
using Xamarin.Forms;

namespace QSF.Examples.MapControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private IShape selectedShape;
        private bool isOpen;
        private string stateName;
        private string stateDensity;

        public FirstLookViewModel()
        {
            this.Densities = new ObservableCollection<DensityItem>();
            this.Densities.Add(new DensityItem("1000+", Color.FromHex("#14333D")));
            this.Densities.Add(new DensityItem("50-100", Color.FromHex("#3C99B7")));
            this.Densities.Add(new DensityItem("500-1000", Color.FromHex("#1E4C5C")));
            this.Densities.Add(new DensityItem("20-50", Color.FromHex("#46B2D6")));
            this.Densities.Add(new DensityItem("200-500", Color.FromHex("#28667A")));
            this.Densities.Add(new DensityItem("0-20", Color.FromHex("#95D3E8")));
            this.Densities.Add(new DensityItem("100-200", Color.FromHex("#327F99")));

            this.ClosePopupCommand = new Command(OnClosePopupCommandExecuted);
        }

        public ObservableCollection<DensityItem> Densities { get; set; }

        public Command ClosePopupCommand { get; set; }

        public IShape SelectedShape
        {
            get
            {
                return this.selectedShape;
            }
            set
            {
                if (this.selectedShape != value)
                {
                    this.selectedShape = value;
                    if (this.selectedShape != null)
                    {
                        this.StateName = this.selectedShape.GetAttribute("STATE_NAME").ToString();
                        this.StateDensity = string.Format("Density: {0}", this.selectedShape.GetAttribute("POP_DENSIT").ToString());
                        this.IsOpen = true;
                    }

                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }
            set
            {
                if (this.isOpen != value)
                {
                    this.isOpen = value;
                    if (!this.isOpen && this.selectedShape != null)
                    {
                        this.SelectedShape = null;
                    }

                    this.OnPropertyChanged();
                }
            }
        }

        public string StateName
        {
            get
            {
                return this.stateName;
            }
            set
            {
                if (this.stateName != value)
                {
                    this.stateName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string StateDensity
        {
            get
            {
                return this.stateDensity;
            }
            set
            {
                if (this.stateDensity != value)
                {
                    this.stateDensity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void OnClosePopupCommandExecuted(object obj)
        {
            this.IsOpen = false;
        }
    }
}