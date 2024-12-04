using System.Diagnostics;
using System.Reflection;

namespace NonExistPlayer.Logging;

public class Logger : IFormatableLogger, IDebugLogger, IConsoleLogger, IFileLogger, ILogger
{
    public const string DefaultFormat = "[%time] [%thread/%TYPE] (%caller): %mes";

    public Logger() { }
    public Logger(Stream? stream)
    {
        if (stream != null)
            FileStream = new StreamWriter(stream);
    }
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
    public TextWriter? FileStream { get; }
    public bool WriteToDebug { get; set; } = false;
    public bool WriteToConsole { get; set; }
    public bool IsConsoleAvaliable { get; private set; } = !OperatingSystem.IsAndroid() || !OperatingSystem.IsIOS();

    public Message Log(string message, ILogLevel? level = null)
    {
        level ??= Default;

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

    public Message Log(object? value, ILogLevel? level = null) => Log(value!.ToString()!, level);

    protected string FormatMessage(string unformatedmessage, ILogLevel level)
    {
        MethodBase method = new StackFrame(1)!.GetMethod()!;
        Thread current = Thread.CurrentThread;

        return OutputFormat
                    .Replace("%time", DateTime.Now.ToString("HH:mm:ss"))
                    .Replace("%thread", current.Name ?? $"Thread #{current.ManagedThreadId}")
                    .Replace("%caller", $"{method.DeclaringType?.Namespace}.{method.Name}")
                    .Replace("%TYPE", level.ToString().ToUpper())
                    .Replace("%Type", level.ToString())
                    .Replace("%type", level.ToString().ToLower())
                    .Replace("%mes", unformatedmessage);
    }

    protected void Log(Message message)
    {
        if (!VerbosityLevel.CanWrite()) return;

        lock (this)
        {
            if (!VerbosityLevel.PreWrite(message)) return;

            if (WriteToDebug && VerbosityLevel.CanWriteToDebug())
                Debug.Write(message.FormattedMessage);

            if (IsConsoleAvaliable && WriteToConsole && VerbosityLevel.CanWriteToConsole())
            {
                try
                {
                    Console.ForegroundColor = message.Color;

                    if (message.IsError)
                        Console.Error.WriteLine(message.FormattedMessage);
                    else
                        Console.WriteLine(message.FormattedMessage);

                    Console.ResetColor();
                }
                catch (IOException)
                {
                    IsConsoleAvaliable = false;
                }
            }

            if (VerbosityLevel.CanWriteToFile())
                FileStream?.WriteLine(message.FormattedMessage);
        }
    }
}