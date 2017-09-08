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

    public static EndpointConfiguration ConfigureEndpoint()
    {
      var epConfig = new EndpointConfiguration("Spike");
      epConfig.UsePersistence<LearningPersistence>();
      epConfig.DisableFeature<TimeoutManager>();
      epConfig.UseTransport<RabbitMQTransport>()
                  .ConnectionString("host=rabbitmq;user=guest;password=guest")
                  .UseConventionalRoutingTopology();

      epConfig.SendFailedMessagesTo("error");
      epConfig.EnableInstallers();
      return epConfig;
    }
  }
}