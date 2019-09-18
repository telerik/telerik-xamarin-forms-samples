using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.ImageEditor;
using Xamarin.Forms;

namespace QSF.Examples.ImageEditorControl.CustomToolbarExample
{
    public class CropToolbarViewModel : BindableBase
    {
        private const string FreeIcon = "\uE86B";
        private const string OriginalIcon = "\uE86C";
        private const string SquareIcon = "\uE86D";
        private const string CircleIcon = "\uE86E";

        private CropTool selectedTool;
        private CropItem selectedItem;

        public CropToolbarViewModel()
        {
            var rectangleGeometry = new RadRectangleGeometry
            {
                Rect = new Rectangle(0.0, 0.0, 1.0, 1.0)
            };

            var ellipseGeometry = new RadEllipseGeometry
            {
                Center = new Point(0.5, 0.5),
                Radius = new Size(0.5, 0.5)
            };

            this.Items = new ObservableCollection<CropItem>
            {
                new CropItem
                {
                    Text = "Free",
                    Icon = FreeIcon,
                    Geometry = rectangleGeometry,
                    AspectRatio = AspectRatio.Free
                },
                new CropItem
                {
                    Text = "Original",
                    Icon = OriginalIcon,
                    Geometry = rectangleGeometry,
                    AspectRatio = AspectRatio.Original
                },
                new CropItem
                {
                    Text = "Square",
                    Icon = SquareIcon,
                    Geometry = rectangleGeometry,
                    AspectRatio = AspectRatio.Square
                },
                new CropItem
                {
                    Text = "Circle",
                    Icon = CircleIcon,
                    Geometry = ellipseGeometry,
                    AspectRatio = AspectRatio.Square
                },
            };

            this.SelectedItem = this.Items.Last();

            this.SelectCommand = new Command<CropItem>(this.OnSelectCommand);
        }

        public CropTool SelectedTool
        {
            get
            {
                return this.selectedTool;
            }
            set
            {
                if (this.selectedTool != value)
                {
                    this.selectedTool = value;
                    this.OnPropertyChanged();
                    this.OnSelectionChanged();
                }
            }
        }

        public CropItem SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged();
                    this.OnSelectionChanged();
                }
            }
        }

        public ObservableCollection<CropItem> Items { get; }

        public ICommand SelectCommand { get; }

        private void OnSelectCommand(CropItem cropItem)
        {
            this.SelectedItem = cropItem;
        }

        private void OnSelectionChanged()
        {
            foreach (var cropItem in this.Items)
            {
                cropItem.IsSelected = cropItem == this.selectedItem;
            }

            if (this.selectedTool != null && this.selectedItem != null)
            {
                this.selectedTool.Geometry = this.selectedItem.Geometry;
                this.selectedTool.AspectRatio = this.selectedItem.AspectRatio;
            }
        }
    }
}
