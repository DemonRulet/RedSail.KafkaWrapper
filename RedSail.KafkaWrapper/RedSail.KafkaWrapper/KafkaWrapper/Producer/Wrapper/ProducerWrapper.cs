using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace RedSail.KafkaWrapper.Producer
{
   
    public class ProducerWrapper : IProducerWrapper
    {
        private readonly IOptions<ProducerConfig> _producerConfig;
        //private readonly ILogger<WrapperProducer> _logger;

        public ProducerWrapper(IOptions<ProducerConfig> producerOptions) { 
            //_logger = logger;
            _producerConfig = producerOptions;
        }

        public async Task<OperationStatus> SendToKafka<TKey, TValue>(Message<TKey, TValue> message, string[] topics)
        {
            ProducerConfig producerConfig = _producerConfig.Value;

            using (IProducer<string, string> producer = new ProducerBuilder<string, string>(producerConfig).Build())
            {
                try
                {
                    foreach (string topic in topics)
                    {
                        DeliveryResult<string, string> result = await producer.ProduceAsync(topic, message.ToJson());
                        //add logging
                    }
                    return OperationStatus.Success;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    //change to logging
                }
            }
            return OperationStatus.Error;
        }

        public async Task<OperationStatus> SendToKafka<TValue>(Message<Null, TValue> message, string[] topics)
        {
            ProducerConfig producerConfig = _producerConfig.Value;

            using (IProducer<Null, string> producer = new ProducerBuilder<Null, string>(producerConfig).Build())
            {
                try
                {
                    foreach (string topic in topics)
                    {
                        DeliveryResult<Null, string> result = await producer.ProduceAsync(topic, message.ToJson());
                        //add logging
                    }
                    return OperationStatus.Success;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    //change to logging
                }
            }
            return OperationStatus.Error;
        }
    }
}
