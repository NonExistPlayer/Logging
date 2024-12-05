namespace NonExistPlayer.Logging;

/// <summary>
/// Represents information about the message that the <see cref="ILogger"/> outputs.
/// </summary>
/// <param name="fMes">Formatted message.</param>
/// <param name="uMes">Unformatted message.</param>
/// <param name="lLvl">Log level.</param>
/// <param name="color">Console color.</param>
public class Message(string? fMes, string uMes, ILogLevel lLvl)
{
    public string? FormattedMessage { get; } = fMes;
    public string UnformattedMessage { get; } = uMes;
    public string? Caller { get; }

    public ILogLevel LogLevel { get; } = lLvl;
    public bool IsError => LogLevel.IsError();
    public ConsoleColor Color => LogLevel.GetColor();
}