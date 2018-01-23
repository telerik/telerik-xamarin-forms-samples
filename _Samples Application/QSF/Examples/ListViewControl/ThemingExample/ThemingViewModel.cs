using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.ListViewControl.ThemingExample
{
    public class ThemingViewModel : ViewModelBase
    {
        public ObservableCollection<string> Items { get; private set; }

        public ThemingViewModel()
        {
            this.Items = new ObservableCollection<string>
            {
                "Vins et alcools Chevalier",
                "Toms Spezialitäten",
                "Hanari Carnes",
                "Victuailles en stock",
                "Suprêmes délices",
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
    }
}
