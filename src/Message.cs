namespace NonExistPlayer.Logging;

public class Message(string? fMes, string uMes, ILogLevel lLvl, ConsoleColor color)
{
    public string? FormatedMessage { get; } = fMes;
    public string UnformatedMessage { get; } = uMes;
    public string? Caller { get; }

    public ILogLevel LogLevel { get; } = lLvl;
    public bool IsError => LogLevel.IsError();
    public ConsoleColor Color { get; } = color;
}