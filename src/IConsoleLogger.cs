namespace NonExistPlayer.Logging;

public interface IConsoleLogger : ILogger
{
    /// <summary>
    /// Should the <see cref="Logger"/> output the log to the console.
    /// </summary>
    bool WriteToConsole { get; set; }
    /// <summary>
    /// Is the console available.
    /// </summary>
    bool IsConsoleAvaliable { get; }
}