using System.Diagnostics;
using System.Reflection;

namespace NonExistPlayer.Logging;

/// <summary>
/// Built-in <see cref="ILogger"/> implementation.
/// </summary>
public class Logger : IFormatableLogger, IDebugLogger, IConsoleLogger, IFileLogger, ILogger
{
    /// <summary>
    /// The output format for default message formatting.
    /// </summary>
    public const string DefaultFormat = "[%time] [%thread/%TYPE] (%namespace.%class.%method): %mes";

    /// <summary>
    /// Initializes the <see cref="Logger"/> class.
    /// </summary>
    public Logger() { }
    /// <summary>
    /// Initializes the <see cref="Logger"/> class with a file stream.
    /// </summary>
    /// <param name="stream">File stream.</param>
    public Logger(Stream? stream)
    {
        if (stream != null)
            FileStream = new StreamWriter(stream);
    }
    /// <summary>
    /// Initializes the <see cref="Logger"/> class with a file stream.
    /// </summary>
    /// <param name="stream">File stream</param>
    public Logger(TextWriter? stream)
    {
        FileStream = stream;
    }

    readonly List<Message> msgs = [];

    public Message[] Messages => [.. msgs];
    public Message Last => msgs.Last();

    public event EventHandler<MessageWritedEventArgs>? MessageWrited;

    public ILogLevel Default { get; set; } = LogLevel.Info;
    public IVerbosityLevel VerbosityLevel { get; set; } = Logging.VerbosityLevel.Maximal;
    public string OutputFormat { get; set; } = DefaultFormat;
    public TextWriter? FileStream { get; set; }
    public bool WriteToDebug { get; set; } = false;
    public bool WriteToConsole { get; set; }
    public bool IsConsoleAvaliable { get; private set; } = !OperatingSystem.IsAndroid() && !OperatingSystem.IsIOS();

    public Message Log(string message, ILogLevel? level = null) => BaseLog(message, level ?? Default);

    public Message Log(object? value, ILogLevel? level = null) => BaseLog(value!.ToString()!, level ?? Default);

    protected string FormatMessage(string unformatedmessage, ILogLevel level, StackFrame frame)
    {
        MethodBase method = frame.GetMethod()!;
        Thread current = Thread.CurrentThread;

        return OutputFormat
                    .Replace("%time", DateTime.Now.ToString("HH:mm:ss"))
                    .Replace("%thread", current.Name ?? $"Thread #{current.ManagedThreadId}")
                    .Replace("%namespace", method.DeclaringType?.Namespace!)
                    .Replace("%class", method.DeclaringType?.Name)
                    .Replace("%method", method.Name)
                    .Replace("%TYPE", level.ToString().ToUpper())
                    .Replace("%Type", level.ToString())
                    .Replace("%type", level.ToString().ToLower())
                    .Replace("%mes", unformatedmessage);
    }

    protected Message BaseLog(string umes, ILogLevel level)
    {
        string fmes = FormatMessage(umes, level, new(2));
        Message message = new(fmes, umes, level);
        if (!VerbosityLevel.CanWrite()) return message;

        lock (this)
        {
            if (!VerbosityLevel.PreWrite(message)) return message;

            if (WriteToDebug && VerbosityLevel.CanWriteToDebug())
                Debug.WriteLine(fmes);

            if (IsConsoleAvaliable && WriteToConsole && VerbosityLevel.CanWriteToConsole())
            {
                try
                {
                    Console.ForegroundColor = message.Color;

                    if (level.IsError())
                        Console.Error.WriteLine(fmes);
                    else
                        Console.WriteLine(fmes);

                    Console.ResetColor();
                }
                catch
                {
                    IsConsoleAvaliable = false;
                }
            }

            if (VerbosityLevel.CanWriteToFile())
                FileStream?.WriteLine(fmes);
        }

        msgs.Add(message);

        MessageWrited?.Invoke(this, new(message));

        return message;
    }
}