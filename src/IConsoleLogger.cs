namespace NonExistPlayer.Logging;

public interface IConsoleLogger : ILogger
{
    bool WriteToConsole { get; set; }
    bool IsConsoleAvaliable { get; }
}