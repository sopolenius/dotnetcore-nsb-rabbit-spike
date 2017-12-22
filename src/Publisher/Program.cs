namespace Publisher
{
    using System;
    using System.Globalization;
    using System.Runtime.Loader;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.MessageMutator;
    using NServiceBus.Transport.RabbitMQ;
    using Shared;

    public class MutateOutgoingMessages :
        IMutateOutgoingMessages
    {
        public MutateOutgoingMessages()
        {
            Console.WriteLine("ctor");
        }
        public async Task MutateOutgoing(MutateOutgoingMessageContext context)
        {
            // the outgoing headers
            var outgoingHeaders = context.OutgoingHeaders;

            if (context.TryGetIncomingMessage(out var incomingMessage))
            {
                // do something with the incoming message
            }

            if (context.TryGetIncomingHeaders(out var incomingHeaders))
            {
                // do something with the incoming headers
            }
            await Task.Yield();
            Console.WriteLine($"Published event, [{Thread.CurrentThread.ManagedThreadId}] User: {Thread.CurrentPrincipal?.Identity?.Name}");

            // the outgoing message
            // optionally replace the message instance by setting context.OutgoingMessage
            var outgoingMessage = context.OutgoingMessage;
        }
    }
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Config.SetupCultures();
            var endpointConfig = Config.ConfigureEndpoint("client",x => {
                x.RouteToEndpoint(typeof(TheCommand),
                "server");
            });
            endpointConfig.SendOnly();
            
            endpointConfig.RegisterComponents(r =>
            {
                r.ConfigureComponent<MutateOutgoingMessages>(DependencyLifecycle.InstancePerCall);
            });
            var stop = new ManualResetEventSlim(false);
            var end = new ManualResetEventSlim(false);
            //epConfig.RegisterMessageMutator(new MutateOutgoingMessages());
            var ep = await Endpoint.Start(endpointConfig).ConfigureAwait(false);
            await Loop(ep).ConfigureAwait(false);

            Console.CancelKeyPress += (x,e) =>{
                stop.Set();
            };
            AssemblyLoadContext.Default.Unloading += ctx =>
            {
                System.Console.WriteLine("Unloding fired");
                stop.Set();
                System.Console.WriteLine("Waiting for completion");
                end.Wait();
            };

            stop.Wait();
            await ep.Stop().ConfigureAwait(false);
            Console.WriteLine("Done");
            end.Set();
        }

        
        static (int a, int b ) Test(){
            return (10, 20);
            Console.WriteLine("Console.WR")
        }


        static async Task Loop(IEndpointInstance endpoint)
        {

            (int x, int y) = Test();
            int i = 0;
            while (true)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("Pingu"), null);
                var publishOptions = new PublishOptions();
                var sendOptions = new SendOptions();
                publishOptions.SetHeader("Test", "Test");

                await Task.Delay(1000);

                await endpoint.Send(new Shared.TheCommand { StringProperty = "Hello", IntProperty = i++ }, sendOptions);

                await Task.Delay(1000);

                await endpoint.Publish(new Shared.TheMessage { StringProperty = "Hello", IntProperty = i++ }, publishOptions);

            }
        }
    }
}
