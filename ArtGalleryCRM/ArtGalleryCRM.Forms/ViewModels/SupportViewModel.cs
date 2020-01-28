using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ArtGalleryCRM.Forms.Services;
using Microsoft.Bot.Connector.DirectLine;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels
{
    public class SupportViewModel : PageViewModelBase
    {
        private ArtGallerySupportBotService _botService;
        private Author _me;
        private Author _supportBot;
        private bool _isBotReady;

        public SupportViewModel()
        {
            this.Title = "Support";
            this.ConversationItems.CollectionChanged += this.ConversationItems_CollectionChanged;
        }

        public ObservableCollection<TextMessage> ConversationItems { get; set; } = new ObservableCollection<TextMessage>();

        public ObservableCollection<Author> TypingAuthors { get; set; } = new ObservableCollection<Author>();

        public Author Me
        {
            get => this._me ?? (this._me = new Author { Name = "Me", Avatar = "ic_chat_user.png" });
            set => SetProperty(ref this._me, value);
        }

        public Author SupportBot
        {
            get => this._supportBot ?? (this._supportBot = new Author { Name = "Support Bot", Avatar = "ic_chat_bot.png" });
            set => SetProperty(ref this._supportBot, value);
        }
        
        #region Methods

        public override async void OnAppearing()
        {
            if (this._isBotReady)
            {
                return;
            }

            this.IsBusy = true;

            this.IsBusyMessage = "starting support service...";

            this._botService = new ArtGallerySupportBotService("Lance");
            this._botService.AttachOnReceiveMessage(this.OnMessageReceived);

            this.IsBusyMessage = "initializing conversation...";

            this.TypingAuthors.Add(SupportBot);

            await _botService.StartConversationAsync().ContinueWith((e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.IsBusyMessage = "";
                    this.IsBusy = false;

                    this._isBotReady = true;

                    this.ConversationItems.Add(new TextMessage { Author = this.SupportBot, Text = "Hi, my name is Luis!" });
                    this.ConversationItems.Add(new TextMessage { Author = this.SupportBot, Text = "I'm a bot, powered by Microsoft Cognitive Services - Natural Language Understanding (LUIS). I've been ML-trained to help you with the features of this application." });
                    this.ConversationItems.Add(new TextMessage { Author = this.SupportBot, Text = "How can I help you? Enter your message below..." });

                    this.TypingAuthors.Clear();
                });
            });
        }

        private void ConversationItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chatMessage = (TextMessage)e.NewItems[0];

                if (chatMessage.Author == this.Me)
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            this.TypingAuthors.Add(SupportBot);
                            this._botService.SendMessage(chatMessage.Text);
                        });

                        return false;
                    });
                }
            }
        }

        private void OnMessageReceived(Activity activity)
        {
            // Clear typing indicators
            this.TypingAuthors.Clear();

            if (activity == null)
            {
                this.ConversationItems.Add(new TextMessage
                {
                    Data = null,
                    Author = this.SupportBot,
                    Text = "ACTIVITY WAS NULL"
                });
                return;
            }
            
            this.ConversationItems.Add(new TextMessage
            {
                Data = null,
                Author = this.SupportBot,
                Text = activity.Text
            });
        }

        #endregion
    }
}