using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using QSF.ViewModels;

namespace QSF.Examples.ProgressBarControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        public List<string> OrderImages { get; } = new List<string>() { "Tiramisu.png", "Belgian_Chocolate.png", "American_Pancakes.png", "Blueberry_Waffle.png" };

        private string orderImage;
        private int orderProgress;
        private int animationDuration = 800;
        private string placeOrderButtonText = "Place Your Order";
        private string statusText = "Proceed to order placement";
        private bool isOrderInProgress = false;

        public FirstLookViewModel()
        {
            this.OrderImage = this.OrderImages.First();
            this.PlaceOrderCommand = new Command(PlaceOrder, CanPlaceOrder);
        }

        public string OrderImage
        {
            get 
            {
                return this.orderImage;}
            set
            {
                if(this.orderImage != value)
                {
                    this.orderImage = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int OrderProgress
        {
            get
            {
                return this.orderProgress;
            }
            set
            {
                if (this.orderProgress != value)
                {
                    this.orderProgress = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int AnimationDuration
        {
            get
            {
                return this.animationDuration;
            }
            set
            {
                if (this.animationDuration != value)
                {
                    this.animationDuration = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string PlaceOrderButtonText
        {
            get
            {
                return this.placeOrderButtonText;
            }
            set
            {
                if (this.placeOrderButtonText != value)
                {
                    this.placeOrderButtonText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string StatusText
        {
            get
            {
                return this.statusText;
            }
            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand PlaceOrderCommand { get; set; }

        private bool CanPlaceOrder()
        {
            return this.isOrderInProgress == false;
        }

        private void PlaceOrder()
        {
            this.ResetOrder();
            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                if (this.AnimationDuration == 0)
                {
                    this.AnimationDuration = 800;
                } 

                this.OrderProgress += 5;

                if (this.OrderProgress == 100)
                {
                    this.CompleteOrder();
                    return false;
                }

                var imageIndex = this.OrderProgress / 25;
                if (this.OrderProgress % 25 == 0)
                {
                    this.OrderImage = this.OrderImages[imageIndex];
                }

                return true;
            });
        }

        public void ResetOrder()
        {
            this.AnimationDuration = 0;
            this.OrderProgress = 0;

            this.isOrderInProgress = true;
            ((Command)this.PlaceOrderCommand).ChangeCanExecute();
            this.PlaceOrderButtonText = "Placing...";
            this.StatusText = "Placing Your Order...";
        }

        public void CompleteOrder()
        {
            this.isOrderInProgress = false;
            ((Command)this.PlaceOrderCommand).ChangeCanExecute();
            this.PlaceOrderButtonText = "Place Another Order";
            this.StatusText = "Your order is placed successfully.";
        }
    }
}
