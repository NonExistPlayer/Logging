namespace NonExistPlayer.Logging;

public class Logger<TLogLevel> :
    Logger,
    ILogger<TLogLevel>,
    IFormatableLogger,
    IDebugLogger,
    IConsoleLogger,
    IFileLogger,
    ILogger
    where TLogLevel : ILogLevel
{
    public Logger(Stream? stream, TLogLevel @default) : base(stream)
    {
        Default = @default;
    }
    public Logger(TextWriter? stream, TLogLevel @default) : base(stream)
    {
        Default = @default;
    }
    public Logger(TLogLevel @default)
    {
        Default = @default;
    }

    readonly List<Message> msgs = [];
    new public event EventHandler<MessageWritedEventArgs>? MessageWrited;
    new public TLogLevel Default { get; set; }

    public TLogLevel? WarningLogLevel { get; set; }
    public TLogLevel? ErrorLogLevel { get; set; }

    new public Message[] Messages => [.. msgs];

    new public Message Log(string message, ILogLevel? level = null)
    {
        throw new NotImplementedException();
    }

    public Message Log(string message) => Log(message, Default);

    public Message Log(string message, TLogLevel level)
    {
        Message mes = new(
            FormatMessage(message, level),
            message,
            level,
            level.GetColor()
        );

        Log(mes);

        msgs.Add(mes);

        MessageWrited?.Invoke(this, new(mes));

        return mes;
    }
    public Message Log(object? value, TLogLevel level) => Log(value!.ToString()!, level);

    public Message Warn(string message) => Log(message, WarningLogLevel ?? throw new NullReferenceException("'WarningLogLevel' was null."));
    public Message Warn(object? value) => Warn(value!.ToString()!);
    public Message Error(string message) => Log(message, ErrorLogLevel ?? throw new NullReferenceException("'ErrorLogLevel' was null."));
    public Message Error(object? value) => Error(value!.ToString()!);
}