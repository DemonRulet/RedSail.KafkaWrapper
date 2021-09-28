using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Confluent.WrapperProducer
{
   
    public class WrapperProducer : IWrapperProducer
    {
        private readonly IOptions<ProducerConfig> _producerConfig;
        //private readonly ILogger<WrapperProducer> _logger;

        public WrapperProducer(IOptions<ProducerConfig> producerOptions) { 
            //_logger = logger;
            _producerConfig = producerOptions;
        }

        private string[] ArrayTopics(string topicStr, char paramSeparator)
        {
            string[] topics = topicStr.Split(paramSeparator, StringSplitOptions.RemoveEmptyEntries);
            return topics.Length > 0 ? topics : null;
        }
        
        public async Task<OperationStatus> SendToKafka<TKey, TValue>(Message<TKey, TValue> message, string topicsStr)
        {
            ProducerConfig producerConfig = _producerConfig.Value;
            string[] topics = ArrayTopics(topicsStr, ';');

            using (IProducer<string, string> producer = new ProducerBuilder<string, string>(producerConfig).Build())
            {
                try
                {
                    foreach (string topic in topics)
                    {
                        DeliveryResult<string, string> result = await producer.ProduceAsync(topic, message.ToJson());
                        Console.WriteLine(result.Key + " " +  result.Message.Value);
                        //add logging
                    }
                    
                    return OperationStatus.Success;
                }
                catch (Exception exception)
                {
                    //change to logging
                    Console.WriteLine(exception.Message);
                }
            }
            return OperationStatus.Error;
        }

        //private Dictionary<string, string> CreateProducerParams(string topic, char paramSeparator, char keyValueSeparator)
        //{
        //    Dictionary<string, string> producerConfig = new();
        //    string[] configParams = topic.Split(paramSeparator, StringSplitOptions.RemoveEmptyEntries);
        //    string[] keyValue;

        //    foreach (string param in configParams)
        //    {
        //        keyValue = param.Split(keyValueSeparator);
        //        producerConfig.Add(keyValue[0], keyValue[1]);
        //    }

        //    return producerConfig;
        //}
    }
}
