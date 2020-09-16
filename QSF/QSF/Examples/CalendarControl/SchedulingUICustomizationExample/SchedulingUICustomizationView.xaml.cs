using System;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulingUICustomizationView : ContentView
    {
        private static ResourceDictionary CustomSchedulingResourceDictionary = new CustomSchedulingResources();
        public SchedulingUICustomizationView()
        {
            InitializeComponent();

            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
                {
                    int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
                    var currentPage = Application.Current.MainPage.Navigation.NavigationStack[index];
                    currentPage.Disappearing += this.CurrentPage_Disappearing;
                    currentPage.Appearing += this.CurrentPage_Appearing;
                }
            });

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.calendar.ScrollTimeIntoView(TimeSpan.FromHours(12));
            }
        }

        private void CurrentPage_Appearing(object sender, System.EventArgs e)
        {
            var applicationMergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (!applicationMergedDictionaries.Contains(CustomSchedulingResourceDictionary))
            {
                Application.Current.Resources.MergedDictionaries.Add(CustomSchedulingResourceDictionary);

                if (Device.RuntimePlatform == Device.UWP)
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
                    {
                        this.calendar.ScrollTimeIntoView(TimeSpan.FromHours(12));
                        return false;
                    });
                }
            }
        }

        private void CurrentPage_Disappearing(object sender, System.EventArgs e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                if (Application.Current.MainPage.Navigation.ModalStack.Count == 0)
                {
                    Application.Current.Resources.MergedDictionaries.Remove(CustomSchedulingResourceDictionary);
                }
            });
        }

        private void Calendar_ViewChanged(object sender, ValueChangedEventArgs<CalendarViewMode> e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                this.calendar.ScrollTimeIntoView(TimeSpan.FromHours(12));
            }
        }
    }
}