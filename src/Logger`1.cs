namespace NonExistPlayer.Logging;

/// <summary>
/// Build-in <see cref="ILogger{TLogLevel}"/> implementation.
/// </summary>
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
    /// <summary>
    /// Initializes the <see cref="Logger{TLogLevel}"/> class.
    /// </summary>
    /// <param name="stream">File stream.</param>
    /// <param name="default">Default log level.</param>
    public Logger(Stream? stream, TLogLevel @default) : base(stream)
    {
        Default = @default;
    }
    /// <summary>
    /// Initializes the <see cref="Logger{TLogLevel}"/> class.
    /// </summary>
    /// <param name="stream">File stream.</param>
    /// <param name="default">Default log level.</param>
    public Logger(TextWriter? stream, TLogLevel @default) : base(stream)
    {
        Default = @default;
    }
    /// <summary>
    /// Initializes the <see cref="Logger{TLogLevel}"/> class.
    /// </summary>
    /// <param name="default">Default log level.</param>
    public Logger(TLogLevel @default)
    {
        Default = @default;
    }

    readonly List<Message> msgs = [];
    new public event EventHandler<MessageWritedEventArgs>? MessageWrited;
    /// <summary>
    /// <inheritdoc cref="ILogger.Default"/>
    /// </summary>
    new public TLogLevel Default { get; set; }

    /// <summary>
    /// The <see cref="TLogLevel"/> used in the <see cref="Warn(string)"/> or <see cref="Warn(object?)"/> method
    /// </summary>
    public TLogLevel? WarningLogLevel { get; set; }
    /// <summary>
    /// The <see cref="TLogLevel"/> used in the <see cref="Error(string)"/> or <see cref="Error(object?)"/> method
    /// </summary>
    public TLogLevel? ErrorLogLevel { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILogger.Messages"/>
    /// </summary>
    new public Message[] Messages => [.. msgs];

    new public Message Log(string message, ILogLevel? level = null)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Logs a <paramref name="message"/>.
    /// </summary>
    public Message Log(string message) => Log(message, Default);
    /// <summary>
    /// Logs a <paramref name="value"/>.
    /// </summary>
    public Message Log(object? value) => Log(value!.ToString()!, Default);

    /// <summary>
    /// <inheritdoc cref="ILogger.Log(string, ILogLevel?)"/>
    /// </summary>
    public Message Log(string message, TLogLevel level)
    {
        Message mes = new(
            FormatMessage(message, level),
            message,
            level,
            level.GetColor()
        );

        base.Log(mes);

        msgs.Add(mes);

        MessageWrited?.Invoke(this, new(mes));

        return mes;
    }
    /// <summary>
    /// <inheritdoc cref="ILogger.Log(object?, ILogLevel?)"/>
    /// </summary>
    public Message Log(object? value, TLogLevel level) => Log(value!.ToString()!, level);

    /// <summary>
    /// Logs a <paramref name="message"/> with the logging level specified in the property <see cref="WarningLogLevel"/>
    /// </summary>
    /// <exception cref="NullReferenceException"/>
    public Message Warn(string message) => Log(message, WarningLogLevel ?? throw new NullReferenceException("'WarningLogLevel' was null."));
    /// <summary>
    /// Logs a <paramref name="value"/> with the logging level specified in the property <see cref="WarningLogLevel"/>
    /// </summary>
    /// <exception cref="NullReferenceException"/>
    public Message Warn(object? value) => Warn(value!.ToString()!);
    /// <summary>
    /// Logs a <paramref name="message"/> with the logging level specified in the property <see cref="ErrorLogLevel"/>
    /// </summary>
    /// <exception cref="NullReferenceException"/>
    public Message Error(string message) => Log(message, ErrorLogLevel ?? throw new NullReferenceException("'ErrorLogLevel' was null."));
    /// <summary>
    /// Logs a <paramref name="value"/> with the logging level specified in the property <see cref="ErrorLogLevel"/>
    /// </summary>
    /// <exception cref="NullReferenceException"/>
    public Message Error(object? value) => Error(value!.ToString()!);
}