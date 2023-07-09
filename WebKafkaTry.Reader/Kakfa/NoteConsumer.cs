using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using WebKafkaTry.Configurations;
using WebKafkaTry.Kafka;
using WebKafkaTry.Proto;

namespace WebKafkaTry.Reader.Kakfa
{
    public class NoteConsumer : INoteConsumer
    {
        private readonly string _topic;
        private readonly IConsumer<long, KafkaNote> _consumer;

        public NoteConsumer(IOptions<KafkaSettings> kafkaSettings)
        {
            if (kafkaSettings.Value.Broker == null)
            {
                throw new ArgumentException("Broker should not be null.", nameof(kafkaSettings));
            }

            _topic = kafkaSettings.Value.Topic ?? throw new ArgumentException("Topic should not be null.", nameof(kafkaSettings));

            var groupId = kafkaSettings.Value.GroupId ?? throw new ArgumentException("GroupId should not be null.", nameof(kafkaSettings));

            var consumerConfig = new ConsumerConfig(){
                BootstrapServers = kafkaSettings.Value.Broker,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<long, KafkaNote>(consumerConfig)
                .SetValueDeserializer(new ProtobufSerializer<KafkaNote>()).Build();
        }

        public void LaunchConsume(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);

            while(!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(cancellationToken);

                //TODO: Send request to save consumed note
                //var consumedNote = consumeResult.Message.Value.FromProto();

            }
            _consumer.Close();
        }
    }
}
