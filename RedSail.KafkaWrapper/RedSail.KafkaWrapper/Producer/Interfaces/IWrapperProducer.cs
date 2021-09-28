using Confluent.Kafka;
using System.Threading.Tasks;

namespace RedSail.KafkaWrapper.Producer
{
    public interface IWrapperProducer 
    {
        public Task<OperationStatus> SendToKafka<TKey, TValue>(Message<TKey, TValue> message, string[] topics); //where TKey : struct;
    }
}
