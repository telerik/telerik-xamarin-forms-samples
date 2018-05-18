using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;

namespace QSF.Examples.ConversationalUIControl.HealthCareAssistanceExample
{
    public class CustomChatItemTemplateSelector : ChatItemTemplateSelector
    {
        public DataTemplate SummaryTemplate { get; set; }
        public DataTemplate WaitingForBotTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ChatItem chatItem = item as ChatItem;
            Summary summary = chatItem?.Data as Summary;
            if (summary != null)
            {
                return this.SummaryTemplate;
            }

            string text = chatItem?.Data as string;
            if (text == HealthCareAssistanceView.WaitingForBot)
            {
                return this.WaitingForBotTemplate;
            }

            return base.OnSelectTemplate(item, container);
        }
    }
}