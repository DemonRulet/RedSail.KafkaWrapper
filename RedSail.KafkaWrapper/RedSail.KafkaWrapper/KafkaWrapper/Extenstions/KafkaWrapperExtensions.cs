using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using RedSail.KafkaWrapper.Consumer;
using RedSail.KafkaWrapper.Producer;
using System;

namespace RedSail.KafkaWrapper.KafkaWrapper
{
    public static class KafkaWrapperExtensions
    {
        public static void AddKafkaWrapper(this IServiceCollection services, Action<ProducerConfig> producerConfig, Action<ConsumerConfig> consumerConfig)
        {
            services.AddConsumerWrapper(consumerConfig);
            services.AddProducerWrapper(producerConfig);
        }
    }
}
