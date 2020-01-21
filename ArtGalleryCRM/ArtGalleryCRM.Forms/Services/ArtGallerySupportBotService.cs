using System;
using System.Linq;
using System.Threading.Tasks;
using ArtGalleryCRM.Forms.Common;
using Microsoft.Bot.Connector.DirectLine;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Services
{
    public class ArtGallerySupportBotService
    {
        private readonly string _user;
        private Action<Activity> _onReceiveMessage;
        private readonly DirectLineClient _client;
        private Conversation _conversation;
        private string _watermark;
        
        public ArtGallerySupportBotService(string userDisplayName)
        {
            this._client = new DirectLineClient(ServiceConstants.DirectLineSecret);
            this._user = userDisplayName;
        }
        
        internal void AttachOnReceiveMessage(Action<Activity> onMessageReceived)
        {
            this._onReceiveMessage = onMessageReceived;
        }

        public async Task StartConversationAsync()
        {
            // **Start a conversation.
            // See https://docs.microsoft.com/en-us/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-start-conversation?view=azure-bot-service-3.0 
            this._conversation = await _client.Conversations.StartConversationAsync();
        }

        public async void SendMessage(string text)
        {
            if(this._conversation == null)
            {
                return;
            }

            // ** Construct and post Activity.
            // See https://docs.microsoft.com/en-us/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-send-activity?view=azure-bot-service-3.0 

            var userMessage = new Activity
            {
                From = new ChannelAccount(this._user),
                Text = text,
                Type = ActivityTypes.Message
            };
            
            await this._client.Conversations.PostActivityAsync(this._conversation.ConversationId, userMessage);

            await this.ReadBotMessagesAsync();
        }

        private async Task ReadBotMessagesAsync()
        {
            // ** Read Bot response by getting the ActivitySet using the watermark (watermark ensures a client will not miss any messages as long as it replays the watermark verbatim)
            // See https://docs.microsoft.com/en-us/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-receive-activities?view=azure-bot-service-3.0
            
            var activitySet = await this._client.Conversations.GetActivitiesAsync(this._conversation.ConversationId, _watermark);

            if (activitySet != null)
            {
                this._watermark = activitySet.Watermark;

                var activities = activitySet.Activities.Where(x => x.From.Id == ServiceConstants.BotId);

                Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (Activity activity in activities)
                    {
                        this._onReceiveMessage?.Invoke(activity);
                    }
                });
            }
        }
    }
}
