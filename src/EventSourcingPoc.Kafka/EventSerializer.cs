using Confluent.Kafka.Serialization;
using EventSourcingPoc.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EventSourcingPoc.Kafka
{
    class EventSerializer<T> : ISerializer<T>, IDeserializer<T>
        where T : Event
    {
        public IEnumerable<KeyValuePair<string, object>> Configure(IEnumerable<KeyValuePair<string, object>> config, bool isKey)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize(string topic, T data)
        {
            return ObjectToByteArray(data);
        }

        public T Deserialize(string topic, byte[] data)
        {
            return ByteArrayToObject(data);
        }

        private static byte[] ObjectToByteArray(T obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private static T ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return (T)binForm.Deserialize(memStream);
            }
        }
    }
}
