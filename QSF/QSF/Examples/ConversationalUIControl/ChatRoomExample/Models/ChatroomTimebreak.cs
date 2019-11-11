﻿namespace QSF.Examples.ConversationalUIControl.ChatRoomExample
{
    public class ChatroomTimebreak : ChatroomMessage
    {
        private string text;

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.UpdateValue(ref this.text, value);
            }
        }
    }
}
