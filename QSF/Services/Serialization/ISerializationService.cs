using System;
using System.IO;

namespace QSF.Services.Serialization
{
    public interface ISerializationService
    {
        T XmlDeserializeFromStream<T>(Stream stream);
    }
}
