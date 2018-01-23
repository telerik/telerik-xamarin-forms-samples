using QSF.ViewModels;
using System.Collections.Generic;

namespace QSF.Examples.AutoCompleteControl.ThemingExample
{
    public class ThemingViewModel : ExampleViewModel
    {
        public ThemingViewModel()
        {
            this.ItemsSource = new List<string>()
            {
                "Vins et alcools Chevalier",
                "Toms Spezialitäten",
                "Hanari Carnes",
                "Victuailles en stock",
                "Suprêmes délices",
                "Hanari Carnes",
                "Chop-suey Chinese",
                "Richter Supermarkt",
                "Wellington Importadora",
                "HILARION-Abastos",
                "Centro comercial Moctezuma",
                "Ottilies Käseladen",
                "Que Delícia",
                "Rattlesnake Canyon Grocery",
                "Ernst Handel",
                "Folk och fä HB",
                "Blondel père et fils",
                "Wartian Herkku",
                "Frankenversand"
            };
        }

        public List<string> ItemsSource { get; }
    }
}
