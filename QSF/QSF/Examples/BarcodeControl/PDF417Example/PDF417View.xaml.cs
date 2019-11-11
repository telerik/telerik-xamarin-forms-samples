using Xamarin.Forms;

namespace QSF.Examples.BarcodeControl.PDF417Example
{
    public partial class PDF417View : ContentView
    {
        private double width;
        private double height;

        public PDF417View()
        {
            this.InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    grid.RowDefinitions.Clear();
                    grid.ColumnDefinitions.Clear();
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    grid.Children.Remove(barcode);
                    grid.Children.Add(barcode, 0, 0);
                    grid.Children.Remove(border);
                    grid.Children.Add(border, 1, 0);
                }
                else
                {
                    grid.RowDefinitions.Clear();
                    grid.ColumnDefinitions.Clear();
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    grid.Children.Remove(barcode);
                    grid.Children.Add(barcode, 0, 0);
                    grid.Children.Remove(border);
                    grid.Children.Add(border, 0, 1);
                }
            }
        }
    }
}