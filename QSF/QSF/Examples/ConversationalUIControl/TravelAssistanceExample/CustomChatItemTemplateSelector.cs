using QSF.Examples.ConversationalUIControl.TravelAssistanceExample.Models;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;

namespace QSF.Examples.ConversationalUIControl.TravelAssistanceExample
{
    public class CustomChatItemTemplateSelector : ChatItemTemplateSelector
    {
        public DataTemplate FlightTemplate { get; set; }
        public DataTemplate SummaryTemplate { get; set; }
        public DataTemplate WaitingForBotTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ChatItem chatItem = item as ChatItem;
            FlightChatContext context = chatItem?.Data as FlightChatContext;
            if (context != null)
            {
                return this.FlightTemplate;
            }

            Summary summary = chatItem?.Data as Summary;
            if (summary != null)
            {
                return this.SummaryTemplate;
            }

            string text = chatItem?.Data as string;
            if (text == TravelBotViewModel.WaitingForBot)
            {
                return this.WaitingForBotTemplate;
            }

            return base.OnSelectTemplate(item, container);
        }
    }
}