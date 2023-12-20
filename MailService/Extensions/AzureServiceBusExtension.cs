
namespace MailService;

public static class AzureServiceBusExtension
{
    public static IAzureServiceBusConsumer azureServiceBusConsumer{get;set;}
    public static IApplicationBuilder UseAzure(this IApplicationBuilder app){
        //Know about the consumer service and also about app lifetime
        azureServiceBusConsumer=app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
        var HostLifeTime=app.ApplicationServices.GetService<IHostApplicationLifetime>();

        HostLifeTime.ApplicationStarted.Register(OnAppStart);
        HostLifeTime.ApplicationStopping.Register(OnAppStopping);
        return app;

    }

    private static void OnAppStopping()
    {
        azureServiceBusConsumer.Stop();
    }

    private static void OnAppStart()
    {
        azureServiceBusConsumer.Start();
    }
}
