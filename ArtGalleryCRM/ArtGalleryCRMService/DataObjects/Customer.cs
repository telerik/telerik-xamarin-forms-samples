using Microsoft.Azure.Mobile.Server;

namespace ArtGalleryCRMService.DataObjects
{
    public class Customer : EntityData
    {
        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string Notes { get; set; }
    }
}