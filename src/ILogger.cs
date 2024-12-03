namespace NonExistPlayer.Logging;

public interface ILogger
{
    Message[] Messages { get; }
    Message Last { get; }
    event EventHandler<MessageWritedEventArgs> MessageWrited;

    ILogLevel Default { get; set; }

    Message Log(string message, ILogLevel? level);
}