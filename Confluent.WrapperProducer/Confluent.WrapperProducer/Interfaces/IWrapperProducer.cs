using Confluent.Kafka;
using System.Threading.Tasks;

namespace Confluent.WrapperProducer
{
    public interface IWrapperProducer 
    {
        public Task<OperationStatus> SendToKafka<TKey, TValue>(Message<TKey, TValue> message, string topicsStr); //where TKey : struct;
    }
}
