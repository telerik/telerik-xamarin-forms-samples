using System;
using Newtonsoft.Json;

namespace ErpApp.Serialization
{
    public class ColorConverter : JsonConverter<Xamarin.Forms.Color>
    {
        public override Xamarin.Forms.Color ReadJson(JsonReader reader, Type objectType, Xamarin.Forms.Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var colorVal = int.Parse(reader.Value.ToString());
            Xamarin.Forms.Color color = Utils.ToColor(colorVal);
            return color;
        }

        public override void WriteJson(JsonWriter writer, Xamarin.Forms.Color value, JsonSerializer serializer)
        {
            System.Drawing.Color sdcolor = Utils.ToColor(value);
            var colorUint = sdcolor.ToArgb();
            writer.WriteValue(colorUint.ToString());
        }
    }
}
