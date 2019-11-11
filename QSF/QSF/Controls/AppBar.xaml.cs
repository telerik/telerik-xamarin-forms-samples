using System.Windows.Input;
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

        public AppBar()
        {
            this.InitializeComponent();
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
    }
}