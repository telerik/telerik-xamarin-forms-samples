using QSF.Services;
using QSF.Services.Serialization;
using Xamarin.Forms;

namespace QSF.Examples.TreeViewControl.Common
{
    public static class DataProvider
    {
        public static T GetData<T>(string path)
        {
            var resourceService = DependencyService.Get<IResourceService>();
            var serializationService = DependencyService.Get<ISerializationService>();

            using (var resourceStream = resourceService.GetResourceStream(path))
            {
                return serializationService.XmlDeserializeFromStream<T>(resourceStream);
            }
        }
    }
}
