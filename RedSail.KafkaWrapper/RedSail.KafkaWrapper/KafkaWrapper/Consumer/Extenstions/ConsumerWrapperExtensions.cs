using System;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace RedSail.KafkaWrapper.Consumer
{
    public static class ConsumerWrapperExtensions
    {
        public static void AddConsumerWrapper(this IServiceCollection services, Action<ConsumerConfig> consumerConfig)
        {
            services.AddScoped<IConsumerWrapper, ConsumerWrapper>();
            services.Configure(consumerConfig);
        }
    }
}
