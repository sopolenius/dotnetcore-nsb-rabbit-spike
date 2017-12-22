namespace Shared
{
  using System;
  using System.Globalization;
  using System.Threading;
  using NServiceBus;
  using NServiceBus.Features;

  public static class Config
  {
    public static void SetupCultures()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
    }

    public static EndpointConfiguration ConfigureEndpoint(string name, Action<RoutingSettings> applyRouting = null)
    {
      var epConfig = new EndpointConfiguration(name);
      epConfig.UsePersistence<LearningPersistence>();
      epConfig.DisableFeature<TimeoutManager>();
      var transport = epConfig.UseTransport<RabbitMQTransport>()
                  .ConnectionString("host=rabbitmq;user=guest;password=guest")
                  .UseConventionalRoutingTopology();
      var routing = transport.Routing();
      
      applyRouting?.Invoke(routing);

      epConfig.SendFailedMessagesTo("error");
      epConfig.EnableInstallers();
      return epConfig;
    }
  }
}