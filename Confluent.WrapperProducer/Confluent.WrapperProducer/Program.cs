using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Confluent.WrapperProducer.TestApplication
{
    public class Program
    {
        public class User
        {
            public string Name { set; get; }
            public int Age { set; get; }
        }
        static async Task Main(string[] args)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
            };

            var topicConfig = new TopicConfig()
            {
                Names = "topic;",
            };

            IWrapperProducer producer = new WrapperProducer(Options.Create(producerConfig), Options.Create(topicConfig));

            await producer.SendToKafka(new Message<int, User>
            {
                Value = new User()
                {
                    Name = "NAME",
                    Age = 16
                },
                Key = 5,
                
            });

        }
    }
}
