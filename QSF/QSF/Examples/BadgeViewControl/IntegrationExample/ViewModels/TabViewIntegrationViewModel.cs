using QSF.ViewModels;

namespace QSF.Examples.BadgeViewControl.IntegrationExample
{
    public class TabViewIntegrationViewModel : ExampleViewModel
    {
        public TabViewIntegrationViewModel()
        {
            this.BreakfastText = "A healthy breakfast has many health benefits, so try not to skip it. Breakfast replenishes the stores of energy and nutrients in your body. People who do not have breakfast may not meet their recommended daily intakes of fibre, vitamins and minerals.";
            this.MainText = "The main dish is usually the heaviest and most complex or substantive dish on a menu. The main ingredient is usually meat or fish; in vegetarian meals, the main course sometimes attempts to mimic a meat course.\n\n It is most often preceded by an appetizer, soup, and/or salad, and followed by a dessert.";
            this.DrinksText = "Drink plenty of water instead of sugary drinks like cordial, energy drinks, sports drinks, fruit drinks, vitamins style waters, flavoured mineral waters and soft drinks.\nDrinks containing added sugars are not required for good health and may increase the risk of weight gain in children and adults.\nSugary drinks contribute to tooth erosion and decay.";
        }

        public string BreakfastText { get; set; }

        public string MainText { get; set; }

        public string DrinksText { get; set; }
    }
}
