using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.ShadowControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        public FirstLookViewModel()
        {
            this.Desserts = new ObservableCollection<Dessert>();

            this.Desserts.Add(new Dessert()
            {
                Title = "American Pancakes",
                Description = "Soft and fluffy American Pancakes, perfect drizzled with maple syrup.",
                ImageName = "American_Pancakes.png"
            });

            this.Desserts.Add(new Dessert()
            {
                Title = "Tiramisu",
                Description = "A creamy dessert of espresso-soaked ladyfingers surrounded by lightly … sweetened whipped cream.",
                ImageName = "Belgian_Chocolate.png"
            });

            this.Desserts.Add(new Dessert()
            {
                Title = "Belgian Chocolate",
                Description = "Ultra-rich chocolate made with cocoa powder, honey and chopped nuts.",
                ImageName = "Blueberry_Waffle.png"
            });

            this.Desserts.Add(new Dessert()
            {
                Title = "Blueberry Waffle",
                Description = "Perfectly crisp and golden on the outside while being light and fluffy on the inside!",
                ImageName = "Tiramisu.png"
            });
        }

        public ObservableCollection<Dessert> Desserts { get; set; }
    }
}

