using Newtonsoft.Json;
using QSF.Examples.ConversationalUIControl.Common;
using QSF.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.ConversationalUIControl.HealthCareAssistanceExample
{
    public partial class HealthCareAssistanceView : ContentView
    {
        public const string WaitingForBot = "WaitingForBot";

        private Author botAuthor;
        private AzureChatBotService botService;
        private ChatMessage waitingForBotMessage;

        public HealthCareAssistanceView()
        {
            InitializeComponent();

            this.busyIndicator.IsBusy = true;
            this.botAuthor = new Author();
            this.botAuthor.Name = "HealthCareBotService";
            string botAvatar = "HealthCareBot.png";
            string suffix = Device.RuntimePlatform == Device.UWP ? "Assets/" : null;
            this.botAuthor.Avatar = suffix + botAvatar;

            this.botService = new AzureChatBotService("yFLWlpeK3CI.cwA.YsY.zgUzyQ1lj2ELKwj7h2pdBqPyVQsf2zrY1DPJfWbWA3I", this.botAuthor.Name, this.OnMessageReceived);

            ((INotifyCollectionChanged)this.chat.Items).CollectionChanged += this.OnChatItemsCollectionChanged;

            this.waitingForBotMessage = new ChatMessage { Author = this.botAuthor, Data = WaitingForBot };
        }

        private void OnChatItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ChatItem chatItem in e.NewItems)
                {
                    TextMessage textMessage = chatItem as TextMessage;
                    if (textMessage != null && textMessage.Author == this.chat.Author)
                    {
                        this.DispatchSendMessageToBot(textMessage.Text);
                    }
                }
            }
        }

        private void DispatchSendMessageToBot(string text)
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.chat.Items.Add(this.waitingForBotMessage);
                    this.botService.SendMessageAsync(this.chat.Author.Name, text);
                });
                return false;
            });
        }

        private void OnMessageReceived(Activity activity)
        {
            if (this.busyIndicator.IsBusy)
            {
                this.busyIndicator.IsBusy = false;
                this.busyIndicator.IsVisible = false;
            }

            this.chat.Items.Remove(this.waitingForBotMessage);

            if (activity.Type != "message")
            {
                return;
            }

            if (!string.IsNullOrEmpty(activity.Text))
            {
                this.HandleInvalidDateSelection(activity.Text);

                TextMessage textMessage = new TextMessage();
                textMessage.Text = activity.Text;
                textMessage.Author = this.botAuthor;

                this.chat.Items.Add(textMessage);
                this.HandleDateSelection(activity);
                this.HandleSuggestedActions(activity);
            }

            if (activity.AttachmentLayout == "carousel")
            {
                this.chat.Items.Add(this.CreateCarouselPickerItem(activity));
            }
            else if (activity.AttachmentLayout == "list" || activity.Attachments != null)
            {
                this.CreatePickerList(activity);
            }
        }

        private void HandleInvalidDateSelection(string message)
        {
            if (message == "This does not seem to be a valid date. Please pick a new one.")
            {
                this.chat.Items.RemoveAt(this.chat.Items.Count - 2);
            }
        }

        private void HandleDateSelection(Activity activity)
        {
            if (activity.Text.Contains("Please, choose a date for visiting"))
            {
                ItemPickerContext itemPickerContext = new ItemPickerContext
                {
                    ItemsSource = new List<CardAction>()
                        {
                            new CardAction() { Title = "Choose a Date" }
                        }
                };

                PickerItem pickerItem = new PickerItem();
                pickerItem.Context = itemPickerContext;

                itemPickerContext.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(ItemPickerContext.SelectedItem))
                    {
                        if (itemPickerContext.SelectedItem != null)
                        {
                            this.chat.Items.Remove(pickerItem);
                            this.chat.Items.Add(this.CreateDatePickerChatItem());
                        }
                    }
                };

                this.chat.Items.Add(pickerItem);
            }
        }

        private ChatItem CreateDatePickerChatItem()
        {
            DatePickerContext datePickercontext = new DatePickerContext();
            PickerItem pickerItem = new PickerItem();
            pickerItem.Context = datePickercontext;

            datePickercontext.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(DatePickerContext.SelectedDate))
                {
                    if (datePickercontext.SelectedDate != null)
                    {
                        this.chat.Items.Remove(pickerItem);
                        string selectedDate = datePickercontext.SelectedDate.Value.ToString("dd MMM", CultureInfo.InvariantCulture);
                        this.chat.Items.Add(new TextMessage() { Author = this.chat.Author, Text = selectedDate });
                    }
                }
            };

            return pickerItem;
        }

        private void HandleSuggestedActions(Activity activity)
        {
            if (activity.SuggestedActions == null)
            {
                return;
            }

            ItemPickerContext itemPickerContext = new ItemPickerContext { ItemsSource = activity.SuggestedActions.Actions };
            PickerItem pickerItem = new PickerItem();
            pickerItem.Context = itemPickerContext;

            itemPickerContext.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ItemPickerContext.SelectedItem))
                {
                    if (itemPickerContext.SelectedItem != null)
                    {
                        this.chat.Items.Remove(pickerItem);
                        string selectedItem = ((CardAction)itemPickerContext.SelectedItem).Title;
                        this.chat.Items.Add(new TextMessage { Author = this.chat.Author, Text = selectedItem });
                    }
                }
            };

            this.chat.Items.Add(pickerItem);
        }

        private void CreatePickerList(Activity activity)
        {
            foreach (Attachment attachment in activity.Attachments)
            {
                if (attachment.ContentType == "application/vnd.microsoft.card.hero")
                {
                    this.CreateHeroCard(attachment);
                }
                else if (attachment.ContentType == "application/vnd.microsoft.card.adaptive")
                {
                    this.CreateAdaptiveCard(attachment);
                    this.HandleSuggestedActions(activity);
                }
            }
        }

        private ChatItem CreateCarouselPickerItem(Activity activity)
        {
            CardPickerContext cardContext = new CardPickerContext();
            List<CardContext> imageCards = new List<CardContext>();
            PickerItem cardPickerItem = new PickerItem();

            foreach (Attachment attachment in activity.Attachments)
            {
                if (attachment.ContentType != "application/vnd.microsoft.card.hero")
                {
                    continue;
                }

                ImageCardContext imageContext = this.CreateImageContext(cardPickerItem, attachment);
                imageCards.Add(imageContext);
            }

            cardContext.Cards = imageCards;
            cardPickerItem.Context = cardContext;

            return cardPickerItem;
        }

        private ImageCardContext CreateImageContext(PickerItem cardPickerItem, Attachment attachment)
        {
            HeroCard card = JsonConvert.DeserializeObject<HeroCard>(attachment.Content.ToString());
            ImageCardContext imageContext = new ImageCardContext();
            if (card.Images != null && card.Images.Count > 0)
            {
                imageContext.Image = card.Images[0].Url;
            }
            imageContext.Title = card.Title;
            imageContext.Subtitle = card.Subtitle;
            imageContext.Description = card.Text;

            foreach (CardAction action in card.Buttons)
            {
                CardAction currentAction = action;
                CardActionContext actionContext = new CardActionContext()
                {
                    Text = currentAction.Title,
                    Command = new Command(() =>
                    {
                        this.chat.Items.Remove(cardPickerItem);
                        this.chat.Items.Add(new TextMessage() { Author = this.chat.Author, Text = currentAction.Value.ToString() });
                    })
                };

                imageContext.Actions.Add(actionContext);
            }

            return imageContext;
        }

        private void CreateHeroCard(Attachment attachment)
        {
            HeroCard heroCard = JsonConvert.DeserializeObject<HeroCard>(attachment.Content.ToString());
            if (heroCard.Buttons == null || heroCard.Buttons.Count == 0)
            {
                return;
            }

            if (heroCard.Text.Contains("What is the convenient time for you on"))
            {
                this.CreateTimePickerHeroCard(heroCard);
            }
            else if (heroCard.Text == "Choose an Insurance Company:")
            {
                this.CreateInsurancePickerHeroCard(heroCard);
            }
            else
            {
                this.CreateItemPickerHeroCard(heroCard);
            }
        }

        private void CreateAdaptiveCard(Attachment attachment)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new JsonAdaptiveElementToObjectConverter());

            AdaptiveCard adaptiveCard = JsonConvert.DeserializeObject<AdaptiveCard>(attachment.Content.ToString(), settings);
            if (adaptiveCard.TType == "Summary")
            {
                string title = ((AdaptiveTextBlock)adaptiveCard.Body[1]).Text;
                string subtitle = ((AdaptiveTextBlock)adaptiveCard.Body[2]).Text;

                StringBuilder description = new StringBuilder();
                description.AppendLine(((AdaptiveTextBlock)adaptiveCard.Body[4]).Text);
                description.AppendLine(((AdaptiveTextBlock)adaptiveCard.Body[3]).Text);
                description.AppendLine(); description.AppendLine(((AdaptiveTextBlock)adaptiveCard.Body[5]).Text);
                description.AppendLine(); description.AppendLine(((AdaptiveTextBlock)adaptiveCard.Body[8]).Text);
                description.AppendLine(string.Format("{0} {1}", ((AdaptiveTextBlock)adaptiveCard.Body[6]).Text, ((AdaptiveTextBlock)adaptiveCard.Body[7]).Text));

                Summary summary = new Summary();
                summary.Title = title;
                summary.Subtitle = subtitle;
                summary.Description = description.ToString();
                summary.ImageUri = ((AdaptiveImage)adaptiveCard.Body[0]).Url.OriginalString;

                ChatItem summaryItem = new PickerItem();
                summaryItem.Data = summary;
                this.chat.Items.Add(summaryItem);
            }
        }

        private void CreateInsurancePickerHeroCard(HeroCard heroCard)
        {
            this.chatPicker.HeaderText = heroCard.Text;
            ItemPickerContext itemPickerContext = new ItemPickerContext() { ItemsSource = heroCard.Buttons };
            this.chatPicker.Context = itemPickerContext;
            this.chatPicker.IsVisible = true;
        }

        private void CreateTimePickerHeroCard(HeroCard heroCard)
        {
            this.chatPicker.HeaderText = heroCard.Text;

            string startTimeText = heroCard.Buttons[0].Value.ToString();
            string endTimeText = heroCard.Buttons[heroCard.Buttons.Count - 1].Value.ToString();

            TimeSpan startTime = DateTime.Parse(startTimeText).TimeOfDay;
            TimeSpan endTime = DateTime.Parse(endTimeText).TimeOfDay;

            TimePickerContext timePickerContext = new TimePickerContext
            {
                StartTime = startTime,
                EndTime = endTime
            };

            this.chatPicker.Context = timePickerContext;
            this.chatPicker.IsVisible = true;
        }

        private void CreateItemPickerHeroCard(HeroCard heroCard)
        {
            this.picker.HeaderText = heroCard.Text;
            ItemPickerContext itemPickerContext = new ItemPickerContext { ItemsSource = heroCard.Buttons };
            itemPickerContext.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ItemPickerContext.SelectedItem))
                {
                    if (itemPickerContext.SelectedItem != null)
                    {
                        this.chat.Items.Add(new TextMessage { Author = this.chat.Author, Text = ((CardAction)itemPickerContext.SelectedItem).Value.ToString() });
                        this.picker.Context = null;
                        this.picker.IsVisible = false;
                    }
                }
            };

            this.picker.Context = itemPickerContext;
            this.picker.IsVisible = true;
        }

        private void OnChatPickerOkClicked(object sender, EventArgs e)
        {
            TimePickerContext context = this.chatPicker.Context as TimePickerContext;
            string message;
            if (context != null)
            {
                message = context.SelectedValue.Value.ToString();
            }
            else
            {
                var action = ((ItemPickerContext)this.chatPicker.Context).SelectedItem;
                message = ((CardAction)action).Value.ToString();
            }

            this.chat.Items.Add(new TextMessage { Author = this.chat.Author, Text = message });
            this.chatPicker.Context = null;
            this.chatPicker.IsVisible = false;
        }

        private void OnChatPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ClassStyle")
            {
                RadChat chat = sender as RadChat;
                if (chat != null)
                {
                    var themesService = DependencyService.Get<IThemesService>();
                    if (themesService.CurrentTheme.Name == "Blue")
                    {
                        chat.Resources["ChatChoiceItemsColor"] = Color.FromHex("#3148CA");
                    }
                    else
                    {
                        chat.Resources["ChatChoiceItemsColor"] = Color.Accent;
                    }
                }
            }
        }
    }
}