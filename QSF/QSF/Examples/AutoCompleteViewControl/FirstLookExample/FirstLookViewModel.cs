using QSF.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QSF.Examples.AutoCompleteViewControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        public FirstLookViewModel()
        {
            this.Source = new ObservableCollection<Contact>()
            {
                new Contact("Joshua Price", "jprice@progress.com", Color.FromHex("#FE6078")),
                new Contact("Reuben Holmes", "rholmes@progress.com",Color.FromHex("#FF714D")),
                new Contact("Eva Lawson", "lthomas@endava.com", Color.FromHex("#FE6078")),
                new Contact("Rory Baxter", "rbaxter@telerik.com", Color.FromHex("#FF714D")),
                new Contact("David Webb", "dwebb@telerk.com", Color.FromHex("#72CDFE")),
                new Contact("Howard Pittman", "hpittman@gmail.com", Color.FromHex("#FE6078")),
                new Contact("Davis Anderson", "danderson@progress.com", Color.FromHex("#FE6078")),
                new Contact("Cannon Puckett", "cpuckett@softserve.com", Color.FromHex("#FF714D")),
                new Contact("Xavi Vasquez", "vasquez@progress.com", Color.FromHex("#FE6078")),
                new Contact("Ronald Hatfield", "rhaltfield@microsoft.com" ,Color.FromHex("#72CDFE")),
                new Contact("Freda Curtis", "fcurtis@telerik.com" ,Color.FromHex("#72CDFE")),
                new Contact("Jeffery Francis", "jfrancis@progress.com", Color.FromHex("#72CDFE")),
                new Contact("Eva Lawson", "elawson@telerik.com", Color.FromHex("#72CDFE")),
                new Contact("Emmett Santos", "esantos@progress.com", Color.FromHex("#FE6078")),
                new Contact("Vada Davies", "vdavies@progress.com", Color.FromHex("#72CDFE")),
                new Contact("Jenny Fuller", "jfuller@gmail.com", Color.FromHex("#FE6078")),
                new Contact("Terrell Norris", "tnorris@telerik.com", Color.FromHex("#FF714D")),
                new Contact("Vadim Saunders", "vsaunders@progress.com", Color.FromHex("#72CDFE")),
                new Contact("Nida Carty", "ncarty@telerik.com", Color.FromHex("#72CDFE")),
                new Contact("Niki Samaniego", "nsamaniego@gmail.com", Color.FromHex("#FF714D")),
                new Contact("Valdex Mills", "vmills@progress.com", Color.FromHex("#FE6078")),
                new Contact("Layton Buck", "lbuck@gmail.com", Color.FromHex("#FF714D")),
                new Contact("Alex Ramos", "aramos@telerik.com", Color.FromHex("#FE6078")),
                new Contact("Alena Cline", "acline@gmail.com", Color.FromHex("#FF714D")),
                new Contact("Joel Walsh", "jwalsh@progress.com", Color.FromHex("#FF714D")),
                new Contact("Vadik Pearson", "vpearson@telerik.com", Color.FromHex("#72CDFE")),
                new Contact("Bob Carty", "bcarty@telerik.com", Color.FromHex("#72CDFE")),
                new Contact("Carol Samaniego", "csamaniego@gmail.com", Color.FromHex("#FF714D")),
                new Contact("Greg Nikolas", "gnikolas@progress.com", Color.FromHex("#FE6078")),
                new Contact("Ivan Ivanov", "iivanov@telerik.com", Color.FromHex("#72CDFE")),
                new Contact("Konny Mills", "kmills@gmail.com", Color.FromHex("#FF714D")),
                new Contact("Matias Santos", "msantos@progress.com", Color.FromHex("#FE6078")),
                new Contact("Peter Bence", "pbence@telerik.com", Color.FromHex("#72CDFE")),
                new Contact("Quincy Sanchez", "qsanchez@gmail.com", Color.FromHex("#FF714D"))
            };
        }

        public ObservableCollection<Contact> Source { get; set; }
    }
}
