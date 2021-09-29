using Confluent.Kafka;
using System.Threading.Tasks;

namespace RedSail.KafkaWrapper.Producer
{
    public interface IProducerWrapper 
    {
        public Task<OperationStatus> SendToKafka<TKey, TValue>(Message<TKey, TValue> message, string[] topics); //where TKey : struct;
        public Task<OperationStatus> SendToKafka<TValue>(Message<Null, TValue> message, string[] topics);
    }
}
