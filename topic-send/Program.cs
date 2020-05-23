using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using queue_send;

namespace topic_send
{
    class Program_
    {
        private static string _bus_connectionstring = "Endpoint=sb://vipinservicebus.servicebus.windows.net/;SharedAccessKeyName=saspolicy;SharedAccessKey=DQq44qgS7+TzbVRgX3wnL0IoQH7usw5voTsF0Oqao1s=";
        private static string _topic_name = "topic-chef";
        private static ITopicClient _client;
        private static string[] Exams = new string[] { "AZ-900", "AZ-300", "AZ-400", "AZ-500", "AZ-204" };
        static async Task Main(string[] args)
        {
            _client = new TopicClient(_bus_connectionstring, _topic_name);

            for (int i = 0; i < 5; i++)
            {
                Order obj = new Order();
                var _message = new Message(Encoding.UTF8.GetBytes(obj.ToString()));
                _message.MessageId = $"{i}";
                _message.UserProperties.Add("Category", Exams[i]);
                await _client.SendAsync(_message);
                Console.WriteLine($"Sending Message : {obj.Id} ");
            }
            Console.ReadKey();
            await _client.CloseAsync();

        }
    }
}
