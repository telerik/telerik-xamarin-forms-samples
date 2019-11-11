using System.Runtime.CompilerServices;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.ConversationalUIControl.ChatRoomExample
{
    public partial class ChatRoomView : ContentView
    {
        public ChatRoomView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "Renderer")
            {
                object renderer = RadPlatform.GetRendererObject(this);
                if (renderer != null)
                {
                    ((ChatroomViewModel)this.BindingContext).StartService();
                }
                else
                {
                    ((ChatroomViewModel)this.BindingContext).StopService();
                }
            }
        }
    }
}