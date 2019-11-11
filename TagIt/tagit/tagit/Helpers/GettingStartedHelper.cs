using System.Collections.Generic;
using tagit.Models;

namespace tagit.Helpers
{
    /// <summary>
    ///     Provides content for Getting Started content
    /// </summary>
    public static class GettingStartedHelper
    {
        public static List<string> SampleImages => new List<string>
        {
            "Image_01.jpg",
            "Image_02.jpg",
            "Image_03.jpg",
            "Image_04.jpg",
            "Image_05.jpg",
            "Image_06.jpg",
            "Image_07.jpg",
            "Image_08.jpg"
        };

        internal static List<GettingStartedInformation> GettingStartedItems
        {
            get
            {   
                var items = new List<GettingStartedInformation>
                {
                    new GettingStartedInformation
                    {
                        Title = "Welcome to Telerik Tagit",
                        Subtitle =
                            "Welcome to Telerik Tagit, the coolest, fastest way to analyze, caption and tag your photos and images.",
                        Image = "gettingstarted_00.png"
                    },
                    new GettingStartedInformation
                    {
                        Title = "Browse Images & Photos",
                        Subtitle = "Browse images and photos from your local camera roll.",
                        Image = "gettingstarted_01.png"
                    },
                    new GettingStartedInformation
                    {
                        Title = "Start Tagging Images",
                        Subtitle = "Automagically have your photos and images analyzed, captioned and tagged.",
                        Image = "gettingstarted_02.png"
                    },
                    new GettingStartedInformation
                    {
                        Title = "Rate, Favorite & Save",
                        Subtitle = "Rate, add to favorites, and even save your tagged images to your device.",
                        Image = "gettingstarted_03.png"
                    },
                    new GettingStartedInformation
                    {
                        Title = "Ready to Get Started?",
                        Subtitle = "Well, what are you waiting for? Start tagging your images and photos now!",
                        Image = "gettingstarted_04.png",
                        IsFinalItem = true
                    }
                };

                return items;
            }
        }
    }
}