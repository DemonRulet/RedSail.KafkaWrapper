using Confluent.Kafka;
using Microsoft.Extensions.Options;
using RedSail.KafkaWrapper.Consumer;
using RedSail.KafkaWrapper.Producer;
using System.Threading.Tasks;

namespace Confluent.TestApplication
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

            IWrapperProducer producer = new WrapperProducer(Options.Create(producerConfig));

            await producer.SendToKafka(new Message<Null, User>
            {
                Value = new User()
                {
                    Name = "Sergey",
                    Age = 20,
                },

            }, new string[] { "topic" });

            IWrapperConsumer consumer = new WrapperConsumer(new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "consumer3",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            });

            consumer.Subscribe("topic", delegate(Message<string, User> message)  
            {
                System.Console.WriteLine(message.Value.Age);
                
            });
        }
    }
}