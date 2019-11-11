using QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample.Models;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;

namespace QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample
{
    public class CustomChatItemTemplateSelector : ChatItemTemplateSelector
    {
        public DataTemplate InsuranceInfoTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ChatItem chatItem = item as ChatItem;
            if (chatItem != null && chatItem.Data is InsuranceInfo)
            {
                return this.InsuranceInfoTemplate;
            }

            return base.OnSelectTemplate(item, container);
        }
    }
}