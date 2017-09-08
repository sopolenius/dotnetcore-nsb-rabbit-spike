namespace Shared
{
    using System;
    using NServiceBus;

    public class TheMessage : IEvent  
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}
