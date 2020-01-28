using Microsoft.Azure.Mobile.Server;

namespace ArtGalleryCRMService.DataObjects
{
    public class Product : EntityData
    {
        public string Title { get; set; }

        public double Price { get; set; }

        public string PhotoUri { get; set; }

        public int InventoryCount { get; set; }

        public bool IsDiscontinued { get; set; }

        public string Description { get; set; }
    }
}