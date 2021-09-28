using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RedSail.KafkaWrapper.Producer
{
    public static class WrapperProducerExtensions
    {
        public static void AddWrapperProducer(this IServiceCollection services, Action<ProducerConfig> producerConfig)
        {
            services.AddScoped<IWrapperProducer, WrapperProducer>();
            services.Configure(producerConfig);
        }
    }
}
