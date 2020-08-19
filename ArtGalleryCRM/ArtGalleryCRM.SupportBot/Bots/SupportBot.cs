using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ArtGalleryCRM.SupportBot.Models;
using ArtGalleryCRM.SupportBot.Services;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.TraceExtensions;
using Microsoft.Bot.Schema;
using Newtonsoft.Json.Linq;

namespace ArtGalleryCRM.SupportBot.Bots
{
    public class SupportBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> context, CancellationToken cancellationToken)
        {
            var message = context.Activity;

            // To quickly test the bot after deploy to ensure the version we want is available.
            if (message.Text.ToLower().Equals("bot version"))
            {
                var version = typeof(SupportBot).Assembly.GetName().Version;

                var replyText = $"Art Gallery Support Assistant v{version?.Major}.{version?.Minor}.{version?.Build}.{version?.Revision}";
                await context.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);

                return;
            }

            // ******************************* Cognitive Services - Text Analysis API ******************************* //

            using var textAnalyticsClient = new TextAnalyticsClient(new TextAnalysisServiceClientCredentials(SubscriptionKeys.TextAnalysisServiceKey))
            {
                Endpoint = "https://eastus.api.cognitive.microsoft.com/"
            };

            var langResult = await textAnalyticsClient.DetectLanguageAsync(false, new LanguageBatchInput(new List<LanguageInput>
            {
                new LanguageInput(id: "1", text: message.Text)
            }), cancellationToken: cancellationToken);

            await context.TraceActivityAsync("OnMessageActivity Trace", langResult, "LanguageResult", cancellationToken: cancellationToken);

            var languageCode = string.Empty;

            foreach (var document in langResult.Documents)
            {
                // Pick the language with the highest score
                var bestLanguage = document.DetectedLanguages?.OrderByDescending(l => l.Score).FirstOrDefault();

                if (string.IsNullOrEmpty(languageCode) && bestLanguage != null)
                {
                    languageCode = bestLanguage.Iso6391Name.ToLower();
                    await context.TraceActivityAsync("OnMessageActivity Trace", languageCode, "string", cancellationToken: cancellationToken);
                }
            }

            // If we couldn't detect language
            if (string.IsNullOrEmpty(languageCode))
            {
                var replyText = "We could not determine what language you're using. Please try again.";
                await context.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
                return;
            }


            // ******************************* Cognitive Services - Text Translation API ******************************* //

            var query = string.Empty;

            // If the detected language was not English, translate it before sending to LUIS
            if (languageCode != "en")
            {
                try
                {
                    using var translationClient = new TextTranslationServiceClient(SubscriptionKeys.TextTranslationServiceKey);

                    var result = await translationClient.TranslateAsync(message.Text, "en");

                    var translatedQuery = result?.Translations?.FirstOrDefault()?.Text;

                    if (!string.IsNullOrEmpty(translatedQuery))
                    {
                        query = translatedQuery;
                    }
                }
                catch (Exception ex)
                {
                    var replyText = $"RespondWithTranslatedReply Exception: {ex.Message}";
                    await context.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
                }
            }
            else
            {
                query = message.Text;
            }


            // ******************************* Cognitive Services - LUIS (Natural Language Understanding) API ******************************* //

            using var luisClient = new LUISRuntimeClient(new ApiKeyServiceClientCredentials(SubscriptionKeys.LuisPredictionKey));

            luisClient.Endpoint = SubscriptionKeys.LuisEndpoint;

            // Prepare a prediction request
            var predictionRequest = new PredictionRequest
            {
                Query = query
            };

            // Request a prediction, returns a PredictionResponse
            var predictionResponse = await luisClient.Prediction.GetSlotPredictionAsync(
                Guid.Parse(SubscriptionKeys.LuisAppId),
                "production",
                predictionRequest,
                verbose: true,
                showAllIntents: true,
                log: true,
                cancellationToken: cancellationToken);

            // You will get a full list of intents. For the purposes of this demo, we'll just use the highest scoring intent.
            var topScoringIntent = predictionResponse.Prediction.TopIntent;

            // Respond to the user depending on the detected intent of their query
            var respondedToQuery = await RespondWithEnglishAsync(context, topScoringIntent, languageCode, cancellationToken);


            // ******************************* Cognitive Services - Sentiment Analysis  ******************************* //

            // Only evaluate sentiment if we've given a meaningful reply (and not connection or service error messages).
            if (respondedToQuery)
            {
                // Use Text Analytics Sentiment analysis
                var sentimentResult = await textAnalyticsClient.SentimentAsync(
                    multiLanguageBatchInput: new MultiLanguageBatchInput(new List<MultiLanguageInput>
                    {
                        new MultiLanguageInput(id:"1", text:query, language:languageCode)
                    }),
                    cancellationToken: cancellationToken);

               
                if (sentimentResult?.Documents?.Count > 0)
                {
                    await context.TraceActivityAsync("SentimentAsync Trace", sentimentResult.Documents[0], "SentimentBatchResultItem", cancellationToken: cancellationToken);

                    SentimentBatchResultItem sentimentItem = sentimentResult.Documents[0];

                    // Use the sentiment score to determine if we need to react, the range is 0 (angriest) to 1 (happiest)
                    await EvaluateAndRespondToSentimentAsync(context, sentimentItem, languageCode, cancellationToken);
                }
            }
        }


        private static async Task<bool> RespondWithEnglishAsync(ITurnContext<IMessageActivity> context, string intent, string languageCode, CancellationToken cancellationToken)
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
                await context.SendActivityAsync(MessageFactory.Text(responseText, responseText), cancellationToken);
                return true;
            }

            var replyText = $"I'm sorry, I wasn't able to understand or locate what you're looking for. Try again with specific questions about employees, customers, products or orders.";
            await context.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);

            return false;
        }

        private static async Task EvaluateAndRespondToSentimentAsync(ITurnContext<IMessageActivity> context, SentimentBatchResultItem sentiment, string languageCode, CancellationToken cancellationToken)
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
                await context.SendActivityAsync(MessageFactory.Text(sentimentResponse, sentimentResponse), cancellationToken);
            }
            catch (Exception ex)
            {
                var replyText = $"EvaluateAndRespondToSentimentAsync Exception: {ex.Message}";
                await context.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
            }
        }

    }
}
