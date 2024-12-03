namespace NonExistPlayer.Logging;

public class MessageWritedEventArgs : EventArgs
{
    internal MessageWritedEventArgs(Message message)
    {
        Message = message;
    }

    public Message Message { get; }
}