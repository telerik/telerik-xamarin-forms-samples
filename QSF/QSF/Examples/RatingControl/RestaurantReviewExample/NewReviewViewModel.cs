using QSF.ViewModels;

namespace QSF.Examples.RatingControl.RestaurantReviewExample
{
    public class NewReviewViewModel : ExampleViewModel
    {
        private string review;
        private double foodRating;
        private double ambienceRating;
        private double serviceRating;

        public string Review
        {
            get
            {
                return this.review;
            }
            set
            {
                if (this.review != value)
                {
                    this.review = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double FoodRating
        {
            get
            {
                return this.foodRating;
            }
            set
            {
                if (this.foodRating != value)
                {
                    this.foodRating = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double AmbienceRating
        {
            get
            {
                return this.ambienceRating;
            }
            set
            {
                if (this.ambienceRating != value)
                {
                    this.ambienceRating = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double ServiceRating
        {
            get
            {
                return this.serviceRating;
            }
            set
            {
                if (this.serviceRating != value)
                {
                    this.serviceRating = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
