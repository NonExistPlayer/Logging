namespace NonExistPlayer.Logging;

public interface IFileLogger : ILogger
{
    TextWriter? FileStream { get; }
}