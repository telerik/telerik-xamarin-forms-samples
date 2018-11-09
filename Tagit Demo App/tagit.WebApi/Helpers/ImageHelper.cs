using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace tagit.WebApi.Helpers
{
    public class ImageHelper
    {
        private static PropertyItem CreatePropertyItem()
        {
            System.Reflection.ConstructorInfo ci = typeof(PropertyItem).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null);
            return (PropertyItem)ci.Invoke(null);
        }

        public static byte[] UpdateImageProperties(Stream stream, string caption, List<string> tags, short ratingValue)
        {
            MemoryStream updatedStream = new MemoryStream();

            short ratingPercentageValue = 0;

            switch (ratingValue)
            {
                case 0: ratingPercentageValue = ratingValue; break;
                case 1: ratingPercentageValue = ratingValue; break;
                default: ratingPercentageValue = (short)((ratingValue - 1) * 25); break;
            }
            
            using (var image = new Bitmap(stream))
            {
                PropertyItem item_title = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
                item_title.Id = 0x9c9b; // XPTitle 0x9c9b
                item_title.Type = 1;
                item_title.Value = Encoding.Unicode.GetBytes(caption + "\0");
                item_title.Len = item_title.Value.Length;
                image.SetPropertyItem(item_title);

                PropertyItem item_title2 = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
                item_title2.Id = 0x010e; // ImageDescription 0x010e
                item_title2.Type = 2;
                item_title2.Value = Encoding.UTF8.GetBytes(caption + "\0");
                item_title2.Len = item_title2.Value.Length;
                image.SetPropertyItem(item_title2);

                PropertyItem rating = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
                rating.Type = 3;
                rating.Id = 18246;
                rating.Value = BitConverter.GetBytes(ratingValue);
                rating.Len = rating.Value.Length;

                PropertyItem ratingPercentage = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
                ratingPercentage.Type = 3;
                ratingPercentage.Id = 18249;
                ratingPercentage.Value = BitConverter.GetBytes(ratingPercentageValue);
                ratingPercentage.Len = ratingPercentage.Value.Length;

                image.SetPropertyItem(rating);
                image.SetPropertyItem(ratingPercentage);

                string tagsValue = string.Join(";", tags);

                PropertyItem keywords = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));

                keywords.Id = 40094;
                keywords.Type = 1;
                keywords.Value = Encoding.Unicode.GetBytes(tagsValue);
                keywords.Len = keywords.Value.Length;
                image.SetPropertyItem(keywords);

                string authorsValue = "Telerik Tagit";

                PropertyItem authors = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));

                authors.Id = 40093;
                authors.Type = 1;
                authors.Value = Encoding.Unicode.GetBytes(authorsValue);
                authors.Len = authors.Value.Length;
                image.SetPropertyItem(authors);


                PropertyItem comments = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
                comments.Id = 40092; // XPTitle 0x9c9b
                comments.Type = 1;
                comments.Value = Encoding.Unicode.GetBytes("Created by Telerik Tagit for Xamarin UI." + "\0");
                comments.Len = comments.Value.Length;
                image.SetPropertyItem(comments);

                //40093
                image.Save(updatedStream, ImageFormat.Jpeg);

                image.Dispose();

            }

            return updatedStream.ToArray();
        }
    }
}