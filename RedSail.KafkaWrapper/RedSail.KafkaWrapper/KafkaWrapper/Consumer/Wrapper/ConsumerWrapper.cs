using Confluent.Kafka;
using System;
using System.Threading;
using System.Text.Json;

namespace RedSail.KafkaWrapper.Consumer
{
    public class ConsumerWrapper : IConsumerWrapper
    {
        private readonly ConsumerConfig _consumerConfig;

        public ConsumerWrapper(ConsumerConfig consumerConfig)
        {
            _consumerConfig = consumerConfig;
        }

        public void Subscribe<TValue>(string topic, IHandler<TValue> handler)
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
                        
                        Message<Null, TValue> message = new()
                        {
                            Value = JsonSerializer.Deserialize<TValue>(messageJson.Message.Value),
                        };

                        handler.Handler(message);
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
