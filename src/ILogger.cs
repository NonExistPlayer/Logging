namespace NonExistPlayer.Logging;

public interface ILogger
{
    /// <summary>
    /// History of displayed messages.
    /// </summary>
    Message[] Messages { get; }
    /// <summary>
    /// The last message that was displayed.
    /// </summary>
    Message Last { get; }
    /// <summary>
    /// An event that is executed when a message is logged.
    /// </summary>
    event EventHandler<MessageWritedEventArgs> MessageWrited;

    /// <summary>
    /// <see cref="ILogLevel"/> which will be used in <see cref="Log(string, ILogLevel?)"/> or <see cref="Log(object?, ILogLevel?)"/> by default.
    /// </summary>
    ILogLevel Default { get; set; }
    /// <summary>
    /// Verbosity level.
    /// </summary>
    IVerbosityLevel VerbosityLevel { get; set; }

    /// <summary>
    /// Logs a <paramref name="message"/> with <paramref name="level"/>.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="level">Log level.</param>
    /// <returns><see cref="Message"/> instance.</returns>
    Message Log(string message, ILogLevel? level);
    /// <summary>
    /// Logs a <paramref name="value"/> with <paramref name="level"/>.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="level">Log level.</param>
    /// <returns><see cref="Message"/> instance.</returns>
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