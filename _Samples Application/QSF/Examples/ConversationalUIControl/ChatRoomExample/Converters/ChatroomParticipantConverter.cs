using Telerik.XamarinForms.ConversationalUI;

namespace QSF.Examples.ConversationalUIControl.ChatRoomExample
{
    public class ChatroomParticipantConverter : IAuthorConverter
    {
        public AuthorsMap AuthorsMap
        {
            get;
            set;
        }

        public Author ConvertToAuthor(object dataItem, AuthorConverterContext context)
        {
            ChatroomParticipant participant = (ChatroomParticipant)dataItem;
            Author author = this.AuthorsMap.GetOrCreateAuthor(participant);
            return author;
        }
    }
}
