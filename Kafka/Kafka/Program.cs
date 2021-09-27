using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Kafka
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

    public class WrapperProducer : IWrapperProducer
    {

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

                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            }
            return null;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            string config = "Server=localhost:9092;Topic=topic;Partition=0;";
            IWrapperProducer wrapperProducer = new WrapperProducer();
            await wrapperProducer.SendToKafka<string, string>(config,"asdasdasdasdasdasd");

        }
    }
}

