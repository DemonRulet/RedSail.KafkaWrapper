using Confluent.Kafka;
using System.Text.Json;

namespace Confluent.WrapperProducer
{
    public static class MapMessageExtensions
    {
        public static Message<TKey, string> ToJson<TKey, TValue>(this Message<TKey, TValue> message)
        {
            return new Message<TKey, string>
            {
                Headers = message.Headers,
                Key = message.Key,
                Timestamp = message.Timestamp,
                Value = JsonSerializer.Serialize(message.Value),
            };
        }
    }
}
