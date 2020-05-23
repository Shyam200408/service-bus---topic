using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;


namespace topic_send
{
    class Program
    {
        private static string _bus_connectionstring = "Endpoint=sb://vipinservicebus.servicebus.windows.net/;SharedAccessKeyName=saspolicy;SharedAccessKey=DQq44qgS7+TzbVRgX3wnL0IoQH7usw5voTsF0Oqao1s=";
        private static string _management_connection_string = "Endpoint=sb://vipinservicebus.servicebus.windows.net/;SharedAccessKeyName=saspolicy;SharedAccessKey=DQq44qgS7+TzbVRgX3wnL0IoQH7usw5voTsF0Oqao1s=;EntityPath=topic-chef";
        private static string _topic_name = "topic-chef";
        private static ITopicClient _client;
        private static string _subscription_name = "SubscriptionC";

        static async Task Main(string[] args)
        {

            CreateSubscription().Wait();
            AddFilter().Wait();
        }

        static async Task CreateSubscription()
        {

            var _management_client = new ManagementClient(_management_connection_string);
            await _management_client.CreateSubscriptionAsync(new SubscriptionDescription(_topic_name, _subscription_name));
            Console.WriteLine("Subscription created");
            Console.ReadLine();

        }

        static async Task AddFilter()
        {
            SubscriptionClient _subscription_client = new SubscriptionClient(_bus_connectionstring, _topic_name, _subscription_name);
            var _subscription_rule = new RuleDescription("MessageRule", new SqlFilter("MessageId='5'"));
            await _subscription_client.AddRuleAsync(_subscription_rule);
            await _subscription_client.RemoveRuleAsync("$Default");
            Console.WriteLine("Rule added");
            Console.ReadLine();

        }

    }
}
