namespace Subscriber
{
    using System;
    using System.Threading.Tasks;

    using NServiceBus;

    using Shared;

    internal class TheMessageHandler : IHandleMessages<TheMessage>
    {
        public Task Handle(TheMessage message, IMessageHandlerContext ctx)
        {
            Console.WriteLine($"Message received: #{message.IntProperty}");
            return Task.FromResult(0);
        }
    }
}