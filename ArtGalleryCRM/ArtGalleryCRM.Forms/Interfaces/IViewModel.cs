using System.Threading.Tasks;

namespace ArtGalleryCRM.Forms.Interfaces
{
    public interface IViewModel
    {
        void OnAppearing();

        bool OnBackButtonRequested();
    }
}