using QSF.Services;
using QSF.Services.Serialization;
using Xamarin.Forms;

namespace QSF.Examples.DataGridControl.Common
{
    public static class DataGenerator
    {
        public static T GetItems<T>(string path)
        {
            var resourceService = DependencyService.Get<IResourceService>();
            var serializationService = DependencyService.Get<ISerializationService>();

            var stream = resourceService.GetResourceStream(path);
            T items = serializationService.XmlDeserializeFromStream<T>(stream);

            return items;
        }
    }
}
