using Confluent.Kafka;

namespace RedSail.KafkaWrapper.Consumer
{
    public interface IHandler<TValue>
    {
        void Handler(Message<Null, TValue> message);
    }
}
