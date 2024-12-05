namespace NonExistPlayer.Logging;

/// <summary>
/// Built-in <see cref="ILogger{TLogLevel, TVerbosityLevel}"/> implementation.
/// </summary>
public class Logger<TLogLevel, TVerbosityLevel> :
    Logger<TLogLevel>,
    ILogger<TLogLevel, TVerbosityLevel>,
    ILogger<TLogLevel>,
    IFormatableLogger,
    IDebugLogger,
    IConsoleLogger,
    IFileLogger,
    ILogger
    where TLogLevel : ILogLevel
    where TVerbosityLevel : IVerbosityLevel
{
    /// <summary>
    /// Initializes the <see cref="Logger{TLogLevel, TVerbosityLevel}"/> class.
    /// </summary>
    /// <param name="stream">File stream.</param>
    /// <param name="default">Default log level.</param>
    /// <param name="vlevel">Verbosity level.</param>
    public Logger(Stream? stream, TLogLevel @default, TVerbosityLevel vlevel) : base(stream, @default)
    {
        VerbosityLevel = vlevel;
    }
    /// <summary>
    /// Initializes the <see cref="Logger{TLogLevel, TVerbosityLevel}"/> class.
    /// </summary>
    /// <param name="stream">File stream.</param>
    /// <param name="default">Default log level.</param>
    /// <param name="vlevel">Verbosity level.</param>
    public Logger(TextWriter? stream, TLogLevel @default, TVerbosityLevel vlevel) : base(stream, @default)
    {
        VerbosityLevel = vlevel;
    }
    /// <summary>
    /// Initializes the <see cref="Logger{TLogLevel, TVerbosityLevel}"/> class.
    /// </summary>
    /// <param name="default">Default log level.</param>
    /// <param name="vlevel">Verbosity level.</param>
    public Logger(TLogLevel @default, TVerbosityLevel vlevel) : base(@default)
    {
        VerbosityLevel = vlevel;
    }

    //// <summary>
    /// <inheritdoc cref="ILogger.VerbosityLevel"/>
    /// </summary>
    new public TVerbosityLevel VerbosityLevel { get; set; }
}