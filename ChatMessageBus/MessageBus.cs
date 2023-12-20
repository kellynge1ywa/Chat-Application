
using Newtonsoft.Json;
using Azure.Messaging.ServiceBus;
using System.Text;

namespace ChatMessageBus;

public class MessageBus : ImessageBus
{
    private readonly string connectionstring="Endpoint=sb://chatbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=qLksmKNHYzYc8lgTzzlfPi06kFmdWwhcs+ASbByE75o=";
    public async Task PublishMessage(object message, string Topic_Queue_Name)
    {
        //Create new client
        var client = new ServiceBusClient(connectionstring);
        ServiceBusSender sender=client.CreateSender(Topic_Queue_Name);

        //Convert the message to json
        var body=JsonConvert.SerializeObject(message);
        ServiceBusMessage busMessage=new ServiceBusMessage(Encoding.UTF8.GetBytes(body)){
            CorrelationId=Guid.NewGuid().ToString(),
        };
        //Send the message
        await sender.SendMessageAsync(busMessage);

        //Free the resource

        await sender.DisposeAsync();
    }
}
