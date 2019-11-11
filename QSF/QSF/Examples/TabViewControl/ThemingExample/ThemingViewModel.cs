using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.TabViewControl.ThemingExample
{
    public class ThemingViewModel : ExampleViewModel
    {
        public ObservableCollection<string> FranceShips { get; private set; }
        public ObservableCollection<string> ItalyShips { get; private set; }
        public ObservableCollection<string> SpainShips { get; private set; }

        public ThemingViewModel()
        {
            this.FranceShips = new ObservableCollection<string>
            {
                "Vins et alcools Chevalier",
                "Victuailles en stock",
                "Blondel père et fils",
                "Du monde entier",
                "Bon app'",
                "La maison d'Asie",
                "Folies gourmandes"
            };
            this.ItalyShips = new ObservableCollection<string>
            {
                "Magazzini Alimentari Riuniti",
                "Reggiani Caseifici",
                "Franchi S.p.A."
            };
            this.SpainShips = new ObservableCollection<string>
            {
                "Romero y tomillo",
                "Godos Cocina Típica",
                "Bólido Comidas preparadas",
                "Galería del gastronómo"
            };
        }
    }
}
