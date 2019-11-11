﻿using System;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace QSF.Services.Serialization
{
    public class SerializationService : ISerializationService
    {
        public T XmlDeserializeFromStream<T>(Stream stream)
        {
            T deserializedObject;
            using (var reader = new StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(T));
                deserializedObject = (T)serializer.Deserialize(reader);
            }

            return deserializedObject;
        }
    }
}
