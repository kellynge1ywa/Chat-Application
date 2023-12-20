namespace ChatMessageBus;

public interface ImessageBus
{
    Task PublishMessage(object message, string Topic_Queue_Name);

}
