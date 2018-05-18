using Newtonsoft.Json.Linq;
using QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample.Models;
using QSF.Services;
using QSF.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsuranceAssistanceView : ContentView
    {
        private Author botAuthor;
        private DialogFlowBotService botService;
        public InsuranceAssistanceView()
        {
            InitializeComponent();

            this.busyIndicator.IsBusy = true;
            this.botAuthor = new Author();
            this.botAuthor.Name = "Botty McBot";
            string botAvatar = "InsuranceBot.png";
            string suffix = Device.RuntimePlatform == Device.UWP ? "Assets/" : null;
            this.botAuthor.Avatar = suffix + botAvatar;

            this.botService = new DialogFlowBotService("280344fb165a461a8ccfef7e1bb47e65");
            ((INotifyCollectionChanged)this.chat.Items).CollectionChanged += this.OnChatItemsCollectionChanged;

            this.SendMessage("restart");
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
                        this.SendMessage(textMessage.Text);
                    }
                }
            }
        }

        private void RenderMessages(Result response)
        {
            if (response == null || response.Fulfillment == null)
            {
                this.busyIndicator.IsVisible = true;
                this.busyIndicator.IsBusy = true;
                return;
            }

            if (this.busyIndicator.IsBusy)
            {
                this.busyIndicator.IsBusy = false;
                this.busyIndicator.IsVisible = false;
            }

            this.RenderFulfillmentMessages(response);
            if (response.Fulfillment.Data != null)
            {
                JToken dataToken = JToken.FromObject(response.Fulfillment.Data);
                if (dataToken.First != null)
                {
                    JToken data = dataToken.First;
                    JToken attachments = data.First["attachments"];
                    this.RenderAttachments(attachments);

                    JToken suggestedActions = data.First["suggestedActions"];
                    if (suggestedActions != null)
                    {
                        this.RenderSuggestedActions(suggestedActions);
                    }
                }
            }
        }

        private void RenderFulfillmentMessages(Result response)
        {
            List<object> messages = response.Fulfillment.Messages;
            foreach (JObject message in messages)
            {
                string textMessage = message.Value<string>("speech");
                if (!string.IsNullOrEmpty(textMessage))
                {
                    this.RenderSpeechMessages(textMessage);
                }
                else
                {
                    this.RenderReplyMessages(message);
                }
            }
        }

        private void RenderReplyMessages(JObject message)
        {
            JArray replies = message.Value<JArray>("replies");

            if (replies == null)
            {
                return;
            }

            List<InsuranceService> options = new List<InsuranceService>();
            foreach (JValue reply in replies)
            {
                InsuranceService option = new InsuranceService();
                option.Type = reply.ToString();
                options.Add(option);
            }

            ItemPickerContext insurancePickerContext = new ItemPickerContext();
            insurancePickerContext.ItemsSource = options;

            PickerItem insurancePickerItem = new PickerItem() { Context = insurancePickerContext };
            insurancePickerContext.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ItemPickerContext.SelectedItem))
                {
                    if (insurancePickerContext.SelectedItem != null)
                    {
                        this.chat.Items.Remove(insurancePickerItem);

                        string insuranceServiceType = ((InsuranceService)insurancePickerContext.SelectedItem).Type;
                        this.chat.Items.Add(new TextMessage() { Author = this.chat.Author, Text = insuranceServiceType });
                    }
                }
            };

            this.chat.Items.Add(insurancePickerItem);
        }

        private void RenderSpeechMessages(string textMessage)
        {
            this.chat.Items.Add(new TextMessage()
            {
                Text = textMessage,
                Author = this.botAuthor
            });

            this.HandleDatePickerResponse(textMessage);
        }

        private void HandleDatePickerResponse(string textMessage)
        {
            if (textMessage.StartsWith("when", StringComparison.OrdinalIgnoreCase))
            {
                DatePickerContext datePickerContext = new DatePickerContext();
                datePickerContext.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(DatePickerContext.SelectedDate))
                    {
                        if (datePickerContext.SelectedDate != null)
                        {
                            this.picker.Context = null;
                            string selectedDate = datePickerContext.SelectedDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                            this.chat.Items.Add(new TextMessage() { Author = this.chat.Author, Text = selectedDate });
                        }
                    }
                };

                this.picker.Context = datePickerContext;
            }
        }

        private void RenderAttachments(JToken attachments)
        {
            int attachmentsCount = attachments.Count();

            if (attachments == null || attachmentsCount == 0)
            {
                return;
            }

            if (attachmentsCount > 1)
            {
                this.RenderCards(attachments);
            }
            else
            {
                this.RenderInsuranceInfo(attachments);
            }
        }

        private void RenderInsuranceInfo(JToken attachments)
        {
            JToken attachment = attachments[0];
            string type = Convert.ToString(attachment["type"]);
            StringBuilder description = new StringBuilder();

            string title = string.Empty;
            if (type == "quote")
            {
                string quote = string.Format("Type: {1}{0}Car model: {2} {3}{0}Car cost: {4}{0}Start date: {5}", Environment.NewLine, attachment["coverage"],
                    attachment["make"], attachment["model"], attachment["worth"], attachment["startDate"]);
                description.Append(quote);

                title = "Your car insurance" + Environment.NewLine + "will be:";
            }
            else if (type == "payment_plan")
            {
                foreach (var row in attachment["rows"])
                {
                    description.AppendLine(string.Format("{0} {1}", row["text"], row["value"]));
                }

                var lastTextMessage = this.chat.Items.LastOrDefault(a => a is ChatMessage && ((ChatMessage)a).Author == this.chat.Author) as TextMessage;
                if (lastTextMessage != null)
                {
                    title = lastTextMessage.Text;
                }
            }

            InsuranceInfo insuranceInfo = new InsuranceInfo();
            insuranceInfo.Title = title;
            insuranceInfo.Description = description.ToString();
            insuranceInfo.TotalValue = string.Format("Total: {0}", attachment["premium"]);

            ChatItem insuranceInfoMessage = new ChatItem();
            insuranceInfoMessage.Data = insuranceInfo;
            this.chat.Items.Add(insuranceInfoMessage);
        }

        private void RenderCards(JToken attachments)
        {
            string layout = Convert.ToString(attachments.Parent.Parent["attachmentLayout"]);

            if (layout != "carousel")
            {
                return;
            }

            List<CardContext> imageCards = new List<CardContext>();
            CardPickerContext cardContext = new CardPickerContext();
            PickerItem cardPickerItem = new PickerItem { Context = cardContext };
            foreach (JToken attachment in attachments)
            {
                List<CardActionContext> actions = new List<CardActionContext>();
                foreach (var action in attachment["buttons"])
                {
                    var localAction = action;
                    actions.Add(new CardActionContext()
                    {
                        Text = Convert.ToString(localAction["title"]),
                        Command = new Command(() =>
                        {
                            this.chat.Items.Remove(cardPickerItem);
                            string actionText = Convert.ToString(localAction["value"]);
                            this.chat.Items.Add(new TextMessage() { Text = actionText, Author = this.chat.Author });
                        })
                    });
                }

                ImageCardContext imageContext = new ImageCardContext();
                if (attachment["images"] != null && attachment["images"].Count() > 0)
                {
                    imageContext.Image = (Convert.ToString(attachment["images"][0]["url"]));
                }

                imageContext.Title = Convert.ToString(attachment["title"]);
                imageContext.Subtitle = Convert.ToString(attachment["subtitle"]);
                imageContext.Actions = actions;

                imageCards.Add(imageContext);
            }

            cardContext.Cards = imageCards;

            this.chat.Items.Add(cardPickerItem);
        }

        private void RenderSuggestedActions(JToken suggestedActions)
        {
            List<SuggestedAction> actions = new List<SuggestedAction>();
            foreach (var action in suggestedActions)
            {
                actions.Add(new SuggestedAction() { Type = Convert.ToString(action["title"]) });
            }

            ItemPickerContext pickerContext = new ItemPickerContext();
            pickerContext.ItemsSource = actions;

            PickerItem pickerItem = new PickerItem() { Context = pickerContext };
            pickerContext.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ItemPickerContext.SelectedItem))
                {
                    if (pickerContext.SelectedItem != null)
                    {
                        this.chat.Items.Remove(pickerItem);

                        string insuranceServiceType = ((SuggestedAction)pickerContext.SelectedItem).Type;
                        this.chat.Items.Add(new TextMessage() { Author = this.chat.Author, Text = insuranceServiceType });
                    }
                }
            };

            this.chat.Items.Add(pickerItem);
        }

        private async void SendMessage(string insuranceServiceType)
        {
            Result response = await this.botService.SendMessageAsync(insuranceServiceType);
            this.RenderMessages(response);
        }

        private void OnChatPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ClassStyle")
            {
                var themesService = DependencyService.Get<IThemesService>();
                if (themesService.CurrentTheme.Name == "Blue")
                {
                    this.Resources["ChatChoiceItemsColor"] = Color.FromHex("#3148CA");
                }
                else
                {
                    this.Resources["ChatChoiceItemsColor"] = Color.Accent;
                }
            }
        }
    }
}