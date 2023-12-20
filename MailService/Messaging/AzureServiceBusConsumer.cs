
using System.Text;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
namespace MailService;

public class AzureServiceBusConsumer : IAzureServiceBusConsumer
{
    private readonly IConfiguration _IConfiguration;
    private readonly string _ConnectionString;
    private readonly string _QueueName;
    private readonly ServiceBusProcessor _emailProcessor;
    private readonly MailsService _MailService;
    
    public AzureServiceBusConsumer(IConfiguration configuration)
    {
        _IConfiguration=configuration;
        _ConnectionString=_IConfiguration.GetValue<string>("AzureConectionString");
        _QueueName=_IConfiguration.GetValue<string>("QueueandTopic:registerqueue");
        _MailService= new MailsService(configuration);

        var client= new ServiceBusClient(_ConnectionString);
        _emailProcessor= client.CreateProcessor(_QueueName);
        
    }
    public async  Task Start()
    {
        _emailProcessor.ProcessMessageAsync+=OnregisterUser;
        _emailProcessor.ProcessErrorAsync +=ErrorHandler;
        await _emailProcessor.StartProcessingAsync();
    }
     public async Task Stop()
    {
        await _emailProcessor.StopProcessingAsync();
        await _emailProcessor.DisposeAsync();
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        //send an email to admin
        return Task.CompletedTask;

    }

    private async  Task OnregisterUser(ProcessMessageEventArgs args)
    {
        var message=args.Message;
        var body=Encoding.UTF8.GetString(message.Body); //read message as string
        var user=JsonConvert.DeserializeObject<ChatUserMessageDto>(body); //string to ChatUserMessageDto

        
        try{
            //Sending an email
            //Confirm email is sent
             StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2014/07/01/15/40/balloon-381334_640.png\" width=\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + user.Fullname + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to T2G Safaris");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>Start your First Adventure!!</p>");

                await _MailService.SendEmail(user, stringBuilder.ToString());

            await args.CompleteMessageAsync(args.Message); //We are done delete message from queue

        } catch(Exception ex){
            throw;
            //send an email to admin

        }
    }

   
}
