﻿using Telerik.XamarinForms.ConversationalUI;

namespace QSF.Examples.ConversationalUIControl.TravelAssistanceExample.Converters
{
    public class ChatItemConverter : IChatItemConverter
    {
        public ChatItem ConvertToChatItem(object dataItem, ChatItemConverterContext context)
        {
            return (ChatItem)dataItem;
        }

        public object ConvertToDataItem(object message, ChatItemConverterContext context)
        {
            TextMessage item = new TextMessage();
            item.Text = message.ToString();
            item.Author = context.Chat.Author;

            return item;
        }
    }
}