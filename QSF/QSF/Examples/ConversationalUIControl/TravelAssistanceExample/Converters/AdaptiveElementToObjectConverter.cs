using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QSF.Examples.ConversationalUIControl.Common;
using System;

namespace QSF.Examples.ConversationalUIControl.TravelAssistanceExample.Converters
{
    public class AdaptiveElementToObjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AdaptiveElement);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            if (jsonObject["type"].Value<string>() == "TextBlock")
            {
                return jsonObject.ToObject<AdaptiveTextBlock>(serializer);
            }
            else if (jsonObject["type"].Value<string>() == "Image")
            {
                return jsonObject.ToObject<AdaptiveImage>(serializer);
            }

            return jsonObject.ToObject<AdaptiveColumnSet>(serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}