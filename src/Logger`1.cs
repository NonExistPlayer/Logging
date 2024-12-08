using System.Diagnostics;

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
    /// <inheritdoc cref="ILogger{TLogLevel}.Log(string, TLogLevel)"/>
    /// </summary>
    public Message Log(string message, TLogLevel level) => BaseLog(message, level);
    /// <summary>
    /// Logs a <paramref name="message"/>.
    /// </summary>
    public Message Log(string message) => BaseLog(message, Default);
    /// <summary>
    /// Logs a <paramref name="value"/>.
    /// </summary>
    public Message Log(object? value) => BaseLog(value!.ToString()!, Default);

    /// <summary>
    /// <inheritdoc cref="ILogger.Log(object?, ILogLevel?)"/>
    /// </summary>
    public Message Log(object? value, TLogLevel level) => BaseLog(value!.ToString()!, level);

    /// <summary>
    /// Logs a <paramref name="message"/> with the logging level specified in the property <see cref="WarningLogLevel"/>
    /// </summary>
    /// <exception cref="NullReferenceException"/>
    public Message Warn(string message) => BaseLog(message, WarningLogLevel ?? throw new NullReferenceException("'WarningLogLevel' was null."));
    /// <summary>
    /// Logs a <paramref name="value"/> with the logging level specified in the property <see cref="WarningLogLevel"/>
    /// </summary>
    /// <exception cref="NullReferenceException"/>
    public Message Warn(object? value) => BaseLog(value!.ToString()!, WarningLogLevel ?? throw new NullReferenceException("'WarningLogLevel' was null."));
    /// <summary>
    /// Logs a <paramref name="message"/> with the logging level specified in the property <see cref="ErrorLogLevel"/>
    /// </summary>
    /// <exception cref="NullReferenceException"/>
    public Message Error(string message) => BaseLog(message, ErrorLogLevel ?? throw new NullReferenceException("'ErrorLogLevel' was null."));
    /// <summary>
    /// Logs a <paramref name="value"/> with the logging level specified in the property <see cref="ErrorLogLevel"/>
    /// </summary>
    /// <exception cref="NullReferenceException"/>
    public Message Error(object? value) => BaseLog(value!.ToString()!, ErrorLogLevel ?? throw new NullReferenceException("'ErrorLogLevel' was null."));
}