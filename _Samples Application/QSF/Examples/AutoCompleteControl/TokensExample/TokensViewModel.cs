using QSF.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace QSF.Examples.AutoCompleteControl.TokensExample
{
    public class TokensViewModel : ExampleViewModel
    {
        private IEnumerable tokens;
        private List<ImageInfo> allImageInfos;
        private List<ImageInfo> imageInfos;

        public TokensViewModel()
        {
            this.allImageInfos = GetData();
            this.Tags = GetTags(this.allImageInfos);
            this.ImageInfos = this.allImageInfos;
        }

        public List<string> Tags
        {
            get;
            private set;
        }

        public IEnumerable Tokens
        {
            get
            {
                return this.tokens;
            }
            set
            {
                if (this.tokens != value)
                {
                    INotifyCollectionChanged observableTokens = this.tokens as INotifyCollectionChanged;
                    if (observableTokens != null)
                    {
                        observableTokens.CollectionChanged -= this.Tokens_CollectionChanged;
                    }

                    this.tokens = value;

                    observableTokens = this.tokens as INotifyCollectionChanged;
                    if (observableTokens != null)
                    {
                        observableTokens.CollectionChanged += this.Tokens_CollectionChanged;
                    }

                    this.OnPropertyChanged();
                    this.OnFilteredTokensChanged();
                }
            }
        }

        public List<ImageInfo> ImageInfos
        {
            get
            {
                return this.imageInfos;
            }
            private set
            {
                if (this.imageInfos != value)
                {
                    this.imageInfos = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private static List<ImageInfo> GetData()
        {
            List<ImageInfo> data = new List<ImageInfo>()
            {
                new ImageInfo("autocomplete_tokens_beach.jpg", "Beach"),
                new ImageInfo("autocomplete_tokens_buenos_aires_hotel.jpg", "Buenos Aires Hotel"),
                new ImageInfo("autocomplete_tokens_bulgaria_sunny_beach.jpg", "Bulgaria Sunny Beach"),
                new ImageInfo("autocomplete_tokens_bulgaria_tevno_lake.jpg", "Bulgaria Tevno Lake"),
                new ImageInfo("autocomplete_tokens_castel_sant_angelo.jpg", "Castel Sant Angelo"),
                new ImageInfo("autocomplete_tokens_church_st_george_attraction.jpg", "Church St George Attraction"),
                new ImageInfo("autocomplete_tokens_featherbed_nature_reserve_attraction.jpg", "Featherbed Nature Reserve Attraction"),
                new ImageInfo("autocomplete_tokens_forbidden_city_attraction.jpg", "Forbidden City Attraction"),
                new ImageInfo("autocomplete_tokens_forest_bulgaria.jpg", "Forest Bulgaria"),
                new ImageInfo("autocomplete_tokens_georgetown_university.jpg", "Georgetown University"),
                new ImageInfo("autocomplete_tokens_key_spices_indian_cuisine.jpg", "Key Spices Indian Cuisine"),
                new ImageInfo("autocomplete_tokens_kustendil_bulgaria.jpg", "Kustendil Bulgaria"),
                new ImageInfo("autocomplete_tokens_leopard_samburu_reserve.jpg", "Leopard Samburu Reserve"),
                new ImageInfo("autocomplete_tokens_lioness_nakuru_national_park_kenya.jpg", "Nakuru National Park Kenya"),
                new ImageInfo("autocomplete_tokens_london_eye_attraction.jpg", "London Eye Attraction"),
                new ImageInfo("autocomplete_tokens_maasai_mara_nature_reserve.jpg", "Maasai Mara Nature Reserve"),
                new ImageInfo("autocomplete_tokens_madison_square_garden_attraction.jpg", "Madison Square Garden Attraction"),
                new ImageInfo("autocomplete_tokens_mahamuni_pagoda_attraction.jpg", "Mahamuni Pagoda Attraction"),
                new ImageInfo("autocomplete_tokens_miraflores_attraction.jpg", "Miraflores Attraction"),
                new ImageInfo("autocomplete_tokens_museum_qin_terracotta_warriors_horses_attraction.jpg", "Museum Of Qin Terracotta Warriors And Horses Attraction"),
                new ImageInfo("autocomplete_tokens_panichishte_bulgaria.jpg", "Panichishte Bulgaria"),
                new ImageInfo("autocomplete_tokens_rent_a_car_or_taxi_in_cuba.jpg", "Rent A Car Or Taxi In Cuba"),
                new ImageInfo("autocomplete_tokens_roman_colosseum_attraction.jpg", "Roman Colosseum Attraction"),
                new ImageInfo("autocomplete_tokens_shwedagon_pagoda_attraction.jpg", "Shwedagon Pagoda Attraction"),
                new ImageInfo("autocomplete_tokens_street_shanghai_china.jpg", "Street Shanghai China"),
                new ImageInfo("autocomplete_tokens_sunny_beach_bulgaria.jpg", "Sunny Beach Bulgaria"),
                new ImageInfo("autocomplete_tokens_sunny_beach_bulgaria_balloon.jpg", "Sunny Beach Bulgaria Balloon"),
                new ImageInfo("autocomplete_tokens_titicaca_lake_attraction.jpg", "Titicaca  Lake  Attraction"),
                new ImageInfo("autocomplete_tokens_tsarevets_sound_and_light_veliko_tarnovo.jpg", "Tsarevets Sound  And Light Veliko Tarnovo"),
                new ImageInfo("autocomplete_tokens_universal_studios_orlando_harry_potter_theme_park.jpg", "Universal Studios Orlando Harry Potter Theme Park"),
            };

            return data;
        }

        private static List<string> GetTags(List<ImageInfo> imageInfos)
        {
            HashSet<string> tags = new HashSet<string>();
            foreach (ImageInfo imageInfo in imageInfos)
            {
                foreach (string tag in imageInfo.ImageTags)
                {
                    tags.Add(tag);
                }
            }

            return tags.OrderBy(t => t).ToList();
        }

        private static HashSet<string> GetTokenSet(IEnumerable filteredTokens)
        {
            HashSet<string> tokenSet = new HashSet<string>();
            foreach (string token in filteredTokens)
            {
                tokenSet.Add(token);
            }
            return tokenSet;
        }

        private static bool HasTag(ImageInfo imageInfo, HashSet<string> tokenSet)
        {
            foreach (string token in tokenSet)
            {
                if (ContainsToken(imageInfo.ImageTags, token))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsToken(HashSet<string> tags, string token)
        {
            foreach (string tag in tags)
            {
                if (string.Equals(tag, token, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnFilteredTokensChanged()
        {
            this.UpdateImageInfos();
        }

        private void Tokens_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateImageInfos();
        }

        private void UpdateImageInfos()
        {
            List<ImageInfo> newInfos = new List<ImageInfo>();

            if (this.tokens != null)
            {
                HashSet<string> tokenSet = GetTokenSet(this.tokens);

                foreach (ImageInfo imageInfo in this.allImageInfos)
                {
                    if (HasTag(imageInfo, tokenSet))
                    {
                        newInfos.Add(imageInfo);
                    }
                }
            }

            if (newInfos.Count == 0)
            {
                newInfos = this.allImageInfos;
            }

            this.ImageInfos = newInfos;
        }
    }
}
