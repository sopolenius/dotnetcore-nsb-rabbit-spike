namespace Subscriber
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
            var ep = await Endpoint.Start(epConfig).ConfigureAwait(false);
        
            var mre = new ManualResetEventSlim(false);
            mre.Wait();
        }
    }
}
