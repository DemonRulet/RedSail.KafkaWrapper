using System;
using Confluent.Kafka;

namespace RedSail.KafkaWrapper.Consumer
{
    public interface IWrapperConsumer
    {
        public void Subscribe<TKey,TValue>(string topic, Action<Message<TKey, TValue>> message);
    }
}
