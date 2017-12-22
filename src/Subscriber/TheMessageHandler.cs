namespace Subscriber
{
    using System;
    using System.Threading.Tasks;

    using NServiceBus;

    using Shared;

    internal class TheMessageHandler : IHandleMessages<TheMessage>, IHandleMessages<TheCommand>
    {
        public Task Handle(TheMessage message, IMessageHandlerContext ctx)
        {
            Console.WriteLine($"Event received: #{message.IntProperty}");
            return Task.CompletedTask;
        }

        public Task Handle(TheCommand message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Command received: #{message.IntProperty}");
            return Task.CompletedTask;
        }
    }
}