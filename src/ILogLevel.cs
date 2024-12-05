namespace NonExistPlayer.Logging;

/// <summary>
/// Represents the level of logging.
/// </summary>
public interface ILogLevel : IEnumAsClass
{
    string ToString();

    /// <summary>
    /// Gets the color for console output depending on the <see cref="IEnumAsClass.Value"/>.
    /// </summary>
    /// <returns></returns>
    ConsoleColor GetColor();

    /// <summary>
    /// Determines whether this logging level is an error.<br/>
    /// This is necessary so that the <see cref="IConsoleLogger"/> can print the error to <c>stderr</c>.
    /// </summary>
    /// <returns>Is the logging level an error.</returns>
    bool IsError();
}