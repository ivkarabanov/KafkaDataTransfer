using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using WebKafkaTry.Configurations;
using WebKafkaTry.Proto;

namespace WebKafkaTry.Reader.Kakfa
{
    public class NoteConsumer
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

            //TODO: вынести GroupId
            var consumerConfig = new ConsumerConfig(){
                BootstrapServers = kafkaSettings.Value.Broker,
                GroupId = "order-consumer",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<long, KafkaNote>(consumerConfig).Build();
        }
    }
}
