using System.Windows.Input;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Controls
{
    public partial class AppBar : TemplatedView
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(AppBar), string.Empty);

        public static readonly BindableProperty TitleIconProperty =
            BindableProperty.Create(nameof(TitleIcon), typeof(string), typeof(AppBar), string.Empty);

        public static readonly BindableProperty TitleHorizontalOptionsProperty =
            BindableProperty.Create(nameof(TitleHorizontalOptions), typeof(LayoutOptions), typeof(AppBar), LayoutOptions.Start);

        public static readonly BindableProperty LeftButtonTextProperty =
            BindableProperty.Create(nameof(LeftButtonText), typeof(string), typeof(AppBar), string.Empty);

        public static readonly BindableProperty LeftButtonCommandProperty =
            BindableProperty.Create(nameof(LeftButtonCommand), typeof(ICommand), typeof(AppBar), null);

        public static readonly BindableProperty IsLeftButtonVisibleProperty =
            BindableProperty.Create(nameof(IsLeftButtonVisible), typeof(bool), typeof(AppBar), true);

        public static readonly BindableProperty IsLeftButtonEnabledProperty =
            BindableProperty.Create(nameof(IsLeftButtonEnabled), typeof(bool), typeof(AppBar), true);

        public static readonly BindableProperty MiddleButtonTextProperty =
            BindableProperty.Create(nameof(MiddleButtonText), typeof(string), typeof(AppBar), string.Empty);

        public static readonly BindableProperty MiddleButtonCommandProperty =
            BindableProperty.Create(nameof(MiddleButtonCommand), typeof(ICommand), typeof(AppBar), null);

        public static readonly BindableProperty IsMiddleButtonVisibleProperty =
            BindableProperty.Create(nameof(IsMiddleButtonVisible), typeof(bool), typeof(AppBar), true);

        public static readonly BindableProperty IsMiddleButtonEnabledProperty =
            BindableProperty.Create(nameof(IsMiddleButtonEnabled), typeof(bool), typeof(AppBar), true);

        public static readonly BindableProperty RightButtonTextProperty =
            BindableProperty.Create(nameof(RightButtonText), typeof(string), typeof(AppBar), string.Empty);

        public static readonly BindableProperty RightButtonCommandProperty =
            BindableProperty.Create(nameof(RightButtonCommand), typeof(ICommand), typeof(AppBar), null);

        public static readonly BindableProperty IsRightButtonVisibleProperty =
            BindableProperty.Create(nameof(IsRightButtonVisible), typeof(bool), typeof(AppBar), true);

        public static readonly BindableProperty IsRightButtonEnabledProperty =
            BindableProperty.Create(nameof(IsRightButtonEnabled), typeof(bool), typeof(AppBar), true);

        public static readonly BindableProperty IsHintOpenProperty =
           BindableProperty.Create(nameof(IsHintOpen), typeof(bool), typeof(AppBar), false,
               propertyChanged: (b, o, n) => ((AppBar)b).IsHintOpenPropertyChanged((bool)n));

        private RadPopup hintPopup;
        private RadButton appBarMiddleButton;

        public AppBar()
        {
            this.InitializeComponent();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.appBarMiddleButton = (RadButton)GetTemplateChild("PART_AppBarMiddleButton");
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public string TitleIcon
        {
            get { return (string)this.GetValue(TitleIconProperty); }
            set { this.SetValue(TitleIconProperty, value); }
        }

        public LayoutOptions TitleHorizontalOptions
        {
            get { return (LayoutOptions)this.GetValue(TitleHorizontalOptionsProperty); }
            set { this.SetValue(TitleHorizontalOptionsProperty, value); }
        }

        public string LeftButtonText
        {
            get { return (string)this.GetValue(LeftButtonTextProperty); }
            set { this.SetValue(LeftButtonTextProperty, value); }
        }

        public ICommand LeftButtonCommand
        {
            get { return (ICommand)this.GetValue(LeftButtonCommandProperty); }
            set { this.SetValue(LeftButtonCommandProperty, value); }
        }

        public bool IsLeftButtonVisible
        {
            get { return (bool)this.GetValue(IsLeftButtonVisibleProperty); }
            set { this.SetValue(IsLeftButtonVisibleProperty, value); }
        }

        public bool IsLeftButtonEnabled
        {
            get { return (bool)this.GetValue(IsLeftButtonEnabledProperty); }
            set { this.SetValue(IsLeftButtonEnabledProperty, value); }
        }

        public string MiddleButtonText
        {
            get { return (string)this.GetValue(MiddleButtonTextProperty); }
            set { this.SetValue(MiddleButtonTextProperty, value); }
        }

        public ICommand MiddleButtonCommand
        {
            get { return (ICommand)this.GetValue(MiddleButtonCommandProperty); }
            set { this.SetValue(MiddleButtonCommandProperty, value); }
        }

        public bool IsMiddleButtonVisible
        {
            get { return (bool)this.GetValue(IsMiddleButtonVisibleProperty); }
            set { this.SetValue(IsMiddleButtonVisibleProperty, value); }
        }

        public bool IsMiddleButtonEnabled
        {
            get { return (bool)this.GetValue(IsMiddleButtonEnabledProperty); }
            set { this.SetValue(IsMiddleButtonEnabledProperty, value); }
        }

        public string RightButtonText
        {
            get { return (string)this.GetValue(RightButtonTextProperty); }
            set { this.SetValue(RightButtonTextProperty, value); }
        }

        public ICommand RightButtonCommand
        {
            get { return (ICommand)this.GetValue(RightButtonCommandProperty); }
            set { this.SetValue(RightButtonCommandProperty, value); }
        }

        public bool IsRightButtonVisible
        {
            get { return (bool)this.GetValue(IsRightButtonVisibleProperty); }
            set { this.SetValue(IsRightButtonVisibleProperty, value); }
        }

        public bool IsRightButtonEnabled
        {
            get { return (bool)this.GetValue(IsRightButtonEnabledProperty); }
            set { this.SetValue(IsRightButtonEnabledProperty, value); }
        }

        public bool IsHintOpen
        {
            get { return (bool)this.GetValue(IsHintOpenProperty); }
            set { this.SetValue(IsHintOpenProperty, value); }
        }

        private void IsHintOpenPropertyChanged(bool isHintOpen)
        {
            if (isHintOpen)
            {
                this.InitializeHintPopup();
                return;
            }

            if (this.hintPopup != null)
            {
                this.hintPopup.IsOpen = false;
            }
        }

        private void InitializeHintPopup()
        {
            this.hintPopup = new RadPopup();
            this.hintPopup.IsModal = false;
            this.hintPopup.OutsideBackgroundColor = Color.FromHex("#BF4B4C4C");
            this.hintPopup.PlacementTarget = this.appBarMiddleButton;
            this.hintPopup.PropertyChanged += this.OnHintPopupPropertyChanged;

            var hintImage = new Image();
            hintImage.Margin = new Thickness(0, 0, 30, 0);
            hintImage.HorizontalOptions = LayoutOptions.End;
            hintImage.VerticalOptions = LayoutOptions.Start;

            hintImage.Source = Device.RuntimePlatform == Device.UWP
                ? new FileImageSource() { File = @"Assets\Config_Text.png" }
                : new FileImageSource() { File = "Config_Text.png" };

            var imageTapGestureRecognizer = new TapGestureRecognizer();
            imageTapGestureRecognizer.Command = new Command(() => this.SetValueCore(AppBar.IsHintOpenProperty, false));
            hintImage.GestureRecognizers.Add(imageTapGestureRecognizer);

            var popupContent = new Grid();
            popupContent.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });

            if (Device.RuntimePlatform == Device.UWP)
            {
                popupContent.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(2, GridUnitType.Star), });
            }

            popupContent.Children.Add(hintImage);
            Grid.SetRow(hintImage, 0);

            this.hintPopup.Content = popupContent;
            this.hintPopup.IsOpen = true;
        }

        private void DismissPopup()
        {
            if (this.hintPopup == null)
            {
                return;
            }

            this.hintPopup.PlacementTarget = null;
            this.hintPopup.PropertyChanged -= this.OnHintPopupPropertyChanged;
            this.hintPopup = null;
        }

        private void OnHintPopupPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOpen" && !this.hintPopup.IsOpen)
            {
                this.DismissPopup();
            }
        }
    }
}