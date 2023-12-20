namespace MailService;

public interface IAzureServiceBusConsumer
{
    Task Start();
    Task Stop();

}
