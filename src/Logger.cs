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
        lock (this)
        {
            if (WriteToDebug)
                Debug.Write(message.FormatedMessage);

            if (IsConsoleAvaliable && WriteToConsole)
            {
                try
                {
                    Console.ForegroundColor = message.Color;

                    if (message.IsError)
                        Console.Error.WriteLine(message.FormatedMessage);
                    else
                        Console.WriteLine(message.FormatedMessage);

                    Console.ResetColor();
                }
                catch (IOException)
                {
                    IsConsoleAvaliable = false;
                }
            }
        }
    }
}

public class Logger<TLogLevel> : Logger, ILogger<TLogLevel>, IFormatableLogger, IDebugLogger, IConsoleLogger, IFileLogger, ILogger where TLogLevel : ILogLevel
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

    public Message Warn(string message)  => Log(message, WarningLogLevel ?? throw new NullReferenceException("'WarningLogLevel' was null."));
    public Message Warn(object? value)   => Warn(value!.ToString()!);
    public Message Error(string message) => Log(message, ErrorLogLevel ?? throw new NullReferenceException("'ErrorLogLevel' was null."));
    public Message Error(object? value)  => Error(value!.ToString()!);
}