namespace Shared
{
    using System;
    using NServiceBus;

    public class TheCommand : IMessage
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
    public class TheMessage : IEvent  
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}
