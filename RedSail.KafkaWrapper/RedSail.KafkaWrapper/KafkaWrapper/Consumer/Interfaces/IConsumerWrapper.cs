namespace RedSail.KafkaWrapper.Consumer
{
    public interface IConsumerWrapper
    {
        public void Subscribe<TValue>(string topic, IHandler<TValue> handler);
    }
}
