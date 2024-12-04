namespace NonExistPlayer.Logging;

public interface ILogger
{
    Message[] Messages { get; }
    Message Last { get; }
    event EventHandler<MessageWritedEventArgs> MessageWrited;

    ILogLevel Default { get; set; }
    IVerbosityLevel VerbosityLevel { get; set; }

    Message Log(string message, ILogLevel? level);
    Message Log(object? value, ILogLevel? level);
}

public interface ILogger<TLogLevel> : ILogger where TLogLevel : ILogLevel
{
    new TLogLevel Default { get; set; }

    TLogLevel? WarningLogLevel { get; set; }
    TLogLevel? ErrorLogLevel { get; set; }

    Message Log(string message, TLogLevel level);
    Message Log(object? value, TLogLevel level);

    Message Warn(string message); Message Warn(object? value);
    Message Error(string message); Message Error(object? value);
}

public interface ILogger<TLogLevel, TVerbosityLevel> :
    ILogger<TLogLevel>,
    ILogger
    where TLogLevel: ILogLevel
    where TVerbosityLevel : IVerbosityLevel
{
    new TVerbosityLevel VerbosityLevel { get; set; }
}