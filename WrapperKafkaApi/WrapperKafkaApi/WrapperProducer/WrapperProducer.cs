using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace WrapperKafkaApi
{
    public interface IWrapperProducer
    {
        Task<DeliveryResult<TKey, TValue>> SendToKafka<TKey, TValue>(string config, TValue value);
    }

    public static class ProducerConstant
    {
        public const string Server = "Server";
        public const string Topic = "Topic";
        public const string Partition = "Partition";
    }

    public class ProducerOptions
    {
        public ProducerConfig producerConfig { set; get; }
        //public string BootstrapServers { set; get; }
        //public string Topic { set; get; }
        //public int Partition { set; get; }
    }
    public class WrapperProducer : IWrapperProducer
    {
        public WrapperProducer(IOptions<ProducerConfig> options)
        {
            Console.WriteLine("yes");
            Console.WriteLine(options.Value.BootstrapServers);
        }
        private Dictionary<string, string> CreateProducerParams(string config, char paramSeparator, char keyValueSeparator)
        {
            Dictionary<string, string> producerConfig = new();
            string[] configParams = config.Split(paramSeparator, StringSplitOptions.RemoveEmptyEntries);
            string[] keyValue;

            foreach (string param in configParams)
            {
                keyValue = param.Split(keyValueSeparator);
                producerConfig.Add(keyValue[0], keyValue[1]);
            }

            return producerConfig;
        }

        public Task<DeliveryResult<TKey, TValue>> SendToKafka<TKey, TValue>(string config, TValue value)
        {
            Dictionary<string, string> producerParams = CreateProducerParams(config, ';', '=');
            ProducerConfig producerConfig = new ProducerConfig()
            {
                BootstrapServers = producerParams[ProducerConstant.Server],
            };

            using (IProducer<TKey, TValue> producer = new ProducerBuilder<TKey, TValue>(producerConfig).Build())
            {
                try
                {
                    TopicPartition topic = new TopicPartition
                    (
                        producerParams[ProducerConstant.Topic],
                        Convert.ToUInt16(producerParams.GetValueOrDefault(ProducerConstant.Partition))

                    );
                    Message<TKey, TValue> message = new Message<TKey, TValue>()
                    {
                        Value = value,
                    };

                    return producer.ProduceAsync(topic, message);
                }

                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            }
            return null;
        }
    }

}
