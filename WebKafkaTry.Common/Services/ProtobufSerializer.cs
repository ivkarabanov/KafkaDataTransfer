using Confluent.Kafka;
using Google.Protobuf;
using System;

namespace WebKafkaTry.Kafka
{
    public class ProtobufSerializer<T> : ISerializer<T>, IDeserializer<T>
    where T : IMessage<T>, new()
    {
        private readonly MessageParser<T> _messageParser;

        public ProtobufSerializer()
        {
            _messageParser = new MessageParser<T>(() => new T());
        }

        public byte[] Serialize(T data, SerializationContext context) => data.ToByteArray();

        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
            {
                throw new ArgumentNullException(nameof(data), "Null data encountered");
            }

            return _messageParser.ParseFrom(data);
        }
    }
}
