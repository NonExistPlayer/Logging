namespace NonExistPlayer.Logging;

public interface IFormatableLogger : ILogger
{
    string OutputFormat { get; set; }
}