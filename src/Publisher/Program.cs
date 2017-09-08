namespace Publisher
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Transport.RabbitMQ;
    using Shared;

  public class Program
    {
        public static void Main(string[] args)
        {
            Config.SetupCultures();
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            var epConfig = Config.ConfigureEndpoint();
            epConfig.SendOnly();

            var ep = await Endpoint.Start(epConfig).ConfigureAwait(false);
            await Loop(ep).ConfigureAwait(false);
            await ep.Stop().ConfigureAwait(false);
        }

        static async Task Loop(IEndpointInstance endpoint)
        {
            int i = 0;
            while (true)
            {
                await endpoint.Publish(new Shared.TheMessage { StringProperty = "Hello", IntProperty = i++ });
                Console.WriteLine($"Published event #{i}");
                Thread.Sleep(200);
            }
        }
    }
}
