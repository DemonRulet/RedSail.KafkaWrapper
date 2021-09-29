using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RedSail.KafkaWrapper.Producer
{
    public static class ProducerWrapperExtensions
    {
        public static void AddProducerWrapper(this IServiceCollection services, Action<ProducerConfig> producerConfig)
        {
            services.AddScoped<IProducerWrapper, ProducerWrapper>();
            services.Configure(producerConfig);
        }
    }
}
