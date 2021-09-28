using Confluent.Kafka;
using System;
using System.Threading;
using System.Text.Json;

namespace RedSail.KafkaWrapper.Consumer
{
    public class WrapperConsumer : IWrapperConsumer
    {
        private readonly ConsumerConfig _consumerConfig;

        public WrapperConsumer(ConsumerConfig consumerConfig)
        {
            _consumerConfig = consumerConfig;
        }

        public void Subscribe<TKey, TValue>(string topic, Action<Message<TKey, TValue>> handler)
        {
            using (var builder = new ConsumerBuilder<string, string>(_consumerConfig).Build())
            {
                builder.Subscribe(topic);

                try
                {
                    CancellationTokenSource cancelToken = new();

                    while (true)
                    {
                        var messageJson = builder.Consume(cancelToken.Token);
                        
                        Message<TKey, TValue> message = new()
                        {
                            Value = JsonSerializer.Deserialize<TValue>(messageJson.Message.Value),
                            Key = JsonSerializer.Deserialize<TKey>(messageJson.Message.Key),
                        };

                        handler(message);
                    }
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    builder.Close();
                }
            }
        }
    }
}
