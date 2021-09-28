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


            IWrapperProducer producer = new WrapperProducer(Options.Create(producerConfig));

            await producer.SendToKafka(new Message<OperationStatus, User>
            {
                Value = new User()
                {
                    Name = "ASDASD",
                    Age = 178
                },
                Key = OperationStatus.Success,
                
            }, "topic;topic1;");

        }
    }
}
