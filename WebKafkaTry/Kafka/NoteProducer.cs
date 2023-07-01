using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebKafkaTry.Configurations;
using WebKafkaTry.Mappers;
using WebKafkaTry.Models;
using WebKafkaTry.Proto;

namespace WebKafkaTry.Kafka
{
    public sealed class NoteProducer : INoteProducer, IDisposable
    {
        private readonly string _topic;
        private readonly IProducer<long, KafkaNote> _producer;

        public NoteProducer(IOptions<KafkaSettings> kafkaSettings)
        {
            if (kafkaSettings.Value.Broker == null)
            {
                throw new ArgumentException("Broker should not be null", nameof(kafkaSettings));
            }

            _topic = kafkaSettings.Value.Topic ?? throw new ArgumentException("Topic should not be null", nameof(kafkaSettings));
            _producer = new ProducerBuilder<long, KafkaNote>(
                    new ProducerConfig()
                    {
                        BootstrapServers = kafkaSettings.Value.Broker,
                        Acks = Acks.Leader
                    })
                .SetValueSerializer(serializer: new ProtobufSerializer<KafkaNote>())
                .Build();
        }

        public async Task ProduceAsync(Note note, CancellationToken cancellationToken)
        {
            var random = new Random();
            await _producer.ProduceAsync(_topic, new Message<long, KafkaNote>
            {
                Key = random.Next(0,1000),
                Value = note.ToKafkaNote()
            }, cancellationToken);
            _producer.Flush(cancellationToken);
        }

        public void Dispose() => _producer.Dispose();
    }
}
