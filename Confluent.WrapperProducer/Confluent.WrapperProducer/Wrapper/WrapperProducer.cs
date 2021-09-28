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
        private readonly IOptions<TopicConfig> _topicConfig;
        //private readonly ILogger<WrapperProducer> _logger;

        public WrapperProducer(IOptions<ProducerConfig> producerOptions, IOptions<TopicConfig> topicConfig) { 
            //_logger = logger;
            _producerConfig = producerOptions;
            _topicConfig = topicConfig;
        }

        private string[] ArrayTopics(string topicStr, char paramSeparator)
        {
            string[] topics = topicStr.Split(paramSeparator, StringSplitOptions.RemoveEmptyEntries);
            return topics.Length > 0 ? topics : null;
        }
        
        public async Task<OperationStatus> SendToKafka<TKey, TValue>(Message<TKey, TValue> message)
        {
            ProducerConfig producerConfig = _producerConfig.Value;
            string[] topicList = ArrayTopics(_topicConfig.Value.Names, ';');

            using (IProducer<TKey, string> producer = new ProducerBuilder<TKey, string>(producerConfig).Build())
            {
                try
                {
                    foreach (string topic in topicList)
                    {
                        DeliveryResult<TKey, string> result = await producer.ProduceAsync(topic, message.ToJson());
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
