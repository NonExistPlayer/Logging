namespace NonExistPlayer.Logging;

public interface IFileLogger : ILogger
{
    /// <summary>
    /// Stream file in which the log will be saved.
    /// </summary>
    TextWriter? FileStream { get; }
}