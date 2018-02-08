using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tagit.ViewModels;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimelineView : ContentView
    {
        public TimelineView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
            {
                this.BindingContext = App.ViewModel.Timeline;

                calendar.AppointmentsSource = App.ViewModel.Timeline.Timeline;

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    ((TimelineViewModel)this.BindingContext)?.Initialize(true);
                    return false;
                });

            }
        }

    }
}