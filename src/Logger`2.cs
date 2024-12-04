namespace NonExistPlayer.Logging;

public class Logger<TLogLevel, TVerbosityLevel> :
    Logger<TLogLevel>,
    ILogger<TLogLevel>,
    IFormatableLogger,
    IDebugLogger,
    IConsoleLogger,
    IFileLogger,
    ILogger
    where TLogLevel : ILogLevel
    where TVerbosityLevel : IVerbosityLevel
{
    public Logger(Stream? stream, TLogLevel @default, TVerbosityLevel vlevel) : base(stream, @default)
    {
        VerbosityLevel = vlevel;
    }
    public Logger(TextWriter? stream, TLogLevel @default, TVerbosityLevel vlevel) : base(stream, @default)
    {
        VerbosityLevel = vlevel;
    }
    public Logger(TLogLevel @default, TVerbosityLevel vlevel) : base(@default)
    {
        VerbosityLevel = vlevel;
    }

    new public TVerbosityLevel VerbosityLevel { get; set; }
}