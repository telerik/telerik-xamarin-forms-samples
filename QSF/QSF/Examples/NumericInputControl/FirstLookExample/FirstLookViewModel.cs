using System.Collections.ObjectModel;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.NumericInputControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private Gender gender;
        private double? age;
        private double? weight;
        private double? height;
        private double? activity;
        private double? calories;

        public Gender Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                if (this.gender != value)
                {
                    this.gender = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double? Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (this.age != value)
                {
                    this.age = value;
                    this.OnPropertyChanged();
                    this.CalculateCommand.ChangeCanExecute();
                }
            }
        }

        public double? Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                if (this.weight != value)
                {
                    this.weight = value;
                    this.OnPropertyChanged();
                    this.CalculateCommand.ChangeCanExecute();
                }
            }
        }

        public double? Height
        {
            get
            {
                return this.height;
            }
            set
            {
                if (this.height != value)
                {
                    this.height = value;
                    this.OnPropertyChanged();
                    this.CalculateCommand.ChangeCanExecute();
                }
            }
        }

        public double? Activity
        {
            get
            {
                return this.activity;
            }
            set
            {
                if (this.activity != value)
                {
                    this.activity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double? Calories
        {
            get
            {
                return this.calories;
            }
            set
            {
                if (this.calories != value)
                {
                    this.calories = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Gender> Genders { get; private set; }
        public Command CalculateCommand { get; private set; }

        public FirstLookViewModel()
        {
            this.Gender = Gender.Female;
            this.Genders = new ObservableCollection<Gender>
            {
                Gender.Female,
                Gender.Male
            };

            this.CalculateCommand = new Command(this.OnCalculateCalories, this.CanCalculateCalories);
        }

        private bool CanCalculateCalories()
        {
            return this.Height.HasValue &&
                   this.Weight.HasValue &&
                   this.Age.HasValue;
        }

        private void OnCalculateCalories()
        {
            var bmr = 10 * 0.45 * this.Weight + 6.25 * 30.48 * this.Height - 5 * this.Age;

            switch (this.Gender)
            {
                case Gender.Female:
                    bmr -= 161;
                    break;
                case Gender.Male:
                    bmr += 5;
                    break;
            }

            switch (this.Activity)
            {
                case 3:
                    this.Calories = bmr * 1.37;
                    break;
                case 4:
                    this.Calories = bmr * 1.42;
                    break;
                case 5:
                    this.Calories = bmr * 1.46;
                    break;
                case 6:
                case 7:
                    this.Calories = bmr * 1.55;
                    break;
                default:
                    this.Calories = bmr * 1.2;
                    break;
            }
        }
    }
}
