using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ArtGalleryCRMSupportBot.Models;
using ArtGalleryCRMSupportBot.Services;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Newtonsoft.Json.Linq;

namespace ArtGalleryCRMSupportBot.Dialogs
{
    [Serializable]
    public class SupportDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            // To quickly test the bot after deploy to ensure the version we want is available.
            if (message.Text.ToLower().Contains("bot version"))
            {
                var version = typeof(SupportDialog).Assembly.GetName().Version;

                await context.PostAsync($"Art Gallery Support Assistant v{version.Major}.{version.Minor}.{version.Build}.{version.Revision}");

                return;
            }


            // ******************************* Cognitive Services - Text Analysis API ******************************* //
            
            var textAnalyticsClient = new TextAnalyticsClient(new TextAnalysisServiceClientCredentials(SubscriptionKeys.TextAnalysisServiceKey))
            {
                Endpoint = "https://eastus.api.cognitive.microsoft.com/"
            };

            // Detect what language is being used
            var langResult = await textAnalyticsClient.DetectLanguageAsync(new BatchInput(new List<Input>
            {
                new Input("1", message.Text)
            }));

            var languageCode = string.Empty;

            foreach (var document in langResult.Documents)
            {
                // Pick the language with the highest score
                var bestLanguage = document.DetectedLanguages?.OrderByDescending(l => l.Score).FirstOrDefault();

                if (string.IsNullOrEmpty(languageCode) && bestLanguage != null)
                {
                    languageCode = bestLanguage.Iso6391Name.ToLower();
                }
            }

            // If we couldn't detect language
            if (string.IsNullOrEmpty(languageCode))
            {
                await context.PostAsync("We could not determine what language you're using. Please try again.");
                return;
            }


            // ******************************* Cognitive Services - Text Translation API ******************************* //

            var query = string.Empty;

            // If the detected language was not English, translate it before sending to LUIS
            if (languageCode != "en")
            {
                try
                {
                    using (var translationClient = new TextTranslationServiceClient(SubscriptionKeys.TextTranslationServiceKey))
                    {
                        var result = await translationClient.TranslateAsync(message.Text, "en");

                        var translatedQuery = result?.Translations?.FirstOrDefault()?.Text;

                        if (!string.IsNullOrEmpty(translatedQuery))
                        {
                            query = translatedQuery;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await context.PostAsync($"RespondWithTranslatedReply Exception: {ex.Message}");
                }
            }
            else
            {
                query = message.Text;
            }


            // ******************************* Cognitive Services - LUIS (Natural Language Understanding) API ******************************* //

            using (var luisClient = new LUISRuntimeClient(new ApiKeyServiceClientCredentials(SubscriptionKeys.LuisPredictionKey)))
            {
                luisClient.Endpoint = SubscriptionKeys.LuisEndpoint;

                // Create prediction client
                var prediction = new Prediction(luisClient);

                // get prediction from LUIS using spell correction and Sentiment enabled (Sentiment is enabled in the LUIS portal Manage -> PublishSettings)
                var luisResult = await prediction.ResolveAsync(
                    appId: SubscriptionKeys.LuisAppId,
                    query: query,
                    timezoneOffset: null,
                    verbose: true,
                    staging: false,
                    spellCheck: true,
                    bingSpellCheckSubscriptionKey: SubscriptionKeys.BingSpellCheckKey,
                    log: false,
                    cancellationToken: CancellationToken.None);

                // You will get a full list of intents. For the purposes of this demo, we'll just use the highest scoring intent.
                var topScoringIntent = luisResult?.TopScoringIntent.Intent;

                // Respond to the user depending on the detected intent of their query
                var respondedToQuery = await RespondWithEnglishAsync(context, topScoringIntent, languageCode);


                // ******************************* Cognitive Services - Sentiment Analysis  ******************************* //

                // You can get the user's sentiment by:
                // 1. Make a separate call to Cognitive Services using "await textAnalyticsClient.SentimentAsync()"
                // or
                // 2. Enable Sentiment in the LUIS portal (Manage -> PublishSettings -> 'Use sentiment analysis')

                // The 2nd option saves you a round trip because sentiment will already be in the LuisResult object!

                // Only evaluate sentiment if we've given a meaningful reply (and not connection or service error messages).
                if (respondedToQuery)
                {
                    // Use the sentiment score, the range is 0 (angriest) to 1 (happiest)
                    await EvaluateAndRespondToSentimentAsync(context, luisResult?.SentimentAnalysis, languageCode);
                }
            }
            
            context.Wait(MessageReceivedAsync);
        }

        private static async Task<bool> RespondWithEnglishAsync(IBotToUser context, string intent, string languageCode)
        {
            var responseText = string.Empty;

            // Intents are capitalized and match the intent's name in the LUIS portal, it's still safer is you use a static
            if (intent == LuisIntents.Greeting)
            {
                responseText = "Hello! Welcome, how can I be of assistance today? You can ask about employees, customers, products, orders or shipping.";
            }
            else if (intent == LuisIntents.Employee)
            {
                responseText = "To locate the employee data, select 'Employees' from the navigation menu. In there you can see a list of employees, selecting one will let you explore employee details and statistics.";
            }
            else if (intent == LuisIntents.Customer)
            {
                responseText = "Choose 'Customers' from the navigation menu. Once there, selecting a customer will open their detailed information, including order history.";
            }
            else if (intent == LuisIntents.Product)
            {
                responseText = "To browse the products list, select 'Products' from the navigation menu. The products page will show all the available art in the gallery.";
            }
            else if (intent == LuisIntents.Order)
            {
                responseText = "Select 'Orders' from the navigation menu. You'll find a DataGrid where you can sort, filter and group orders. Order details will provide you with customer and employee data as well.";
            }
            else if (intent == LuisIntents.Shipping)
            {
                responseText = "Find all scheduled packages to be delivered on the Shipping page. You'll see the order and the carrier that is delivering it.";
            }
            else if (intent == LuisIntents.Help)
            {
                responseText = "Sure, let me try to help. You can ask about the employees, the products, customers, orders and shipping information. If you're still stuck, call us at 1-800-123-4567.";
            }
            else if (intent == LuisIntents.None)
            {
                responseText = "I'm not sure what I can to help with this. You might need a human, contact us at support@yourcompany.com";
            }
            else if (intent == LuisIntents.Cancel)
            {
                responseText = "If you would like to cancel and end this conversation, just navigate to another page in the application.";
            }
            else if (intent == LuisIntents.Joke)
            {
                var joke = "";

                string url = "http://api.icndb.com/jokes/random?exclude=[explicit]&limitTo=[nerdy]";

                using (var client = new HttpClient())
                {
                    var json = await client.GetStringAsync(url);

                    dynamic jokeResult = JObject.Parse(json);

                    if (jokeResult?.type == "success")
                    {
                        joke = jokeResult.value.joke;
                    }
                }

                responseText = string.IsNullOrEmpty(joke) 
                    ? "My funny bone doesn't appear to be working right now. Try again later :D" 
                    : joke;
            }

            // If the user is using another language, reply in their language
            if (languageCode != "en")
            {
                using (var translationClient = new TextTranslationServiceClient(SubscriptionKeys.TextTranslationServiceKey))
                {
                    var translationResult = await translationClient.TranslateAsync(responseText, languageCode);

                    if (translationResult != null)
                    {
                        var translatedReply = translationResult?.Translations?.FirstOrDefault()?.Text;

                        if (!string.IsNullOrEmpty(translatedReply))
                        {
                            responseText = translatedReply;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(responseText))
            {
                await context.PostAsync(responseText);
                return true;
            }

            await context.PostAsync("I'm sorry, I wasn't able to understand or locate what you're looking for. Try again with specific questions about employees, customers, products or orders.");

            return false;
        }

        private static async Task EvaluateAndRespondToSentimentAsync(IBotToUser context, Sentiment sentiment, string languageCode)
        {
            // If there's no Sentiment object or score value, quit now.
            if (sentiment?.Score == null)
            {
                return;
            }

            var sentimentResponse = string.Empty;

            try
            {
                // Score ranges from 0.0 to 1, with 4 decimals of precision
                if (sentiment.Score <= 0.1)
                {
                    // If the sentiment is lower than 3%, respond with apologies and recommend to reach out to support
                    sentimentResponse = "I'm sorry you're having problems. If you'd like to talk to a person, please email support@company.com and they will be able to assist further.";
                }
                else if (sentiment.Score >= 0.95)
                {
                    // if the sentiment is in the top 97%, respond with a reminder to leave a review for the app.
                    sentimentResponse = "I'm happy to see you're enjoying our services. Please consider leaving an app review after you're done today!";
                }

                // If there's no response needed, quit now.
                if (string.IsNullOrEmpty(sentimentResponse))
                {
                    return;
                }
                
                // Check to see if we need to translate the response.
                if (languageCode != "en")
                {
                    using (var translationClient = new TextTranslationServiceClient(SubscriptionKeys.TextTranslationServiceKey))
                    {
                        if (!string.IsNullOrEmpty(sentimentResponse))
                        {
                            var translationResult = await translationClient.TranslateAsync(sentimentResponse, languageCode);

                            var translatedSentimentResponse = translationResult.Translations.FirstOrDefault()?.Text;

                            if (!string.IsNullOrEmpty(translatedSentimentResponse))
                            {
                                // If we were able to translate the message, update the message we're sending.
                                sentimentResponse = translatedSentimentResponse;
                            }
                        }
                    }
                }

                // Reply with the sentiment response.
                await context.PostAsync(sentimentResponse);
            }
            catch (Exception ex)
            {
                await context.PostAsync($"EvaluateAndRespondToSentimentAsync Exception: {ex.Message}");
            }
        }
    }
}