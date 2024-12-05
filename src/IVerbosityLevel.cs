namespace NonExistPlayer.Logging;

/// <summary>
/// Represents the level of verbosity.
/// </summary>
public interface IVerbosityLevel : IEnumAsClass
{
    /// <summary>
    /// A method that should be called in the <see cref="ILogger"/> before logging a message.
    /// </summary>
    /// <returns>Is logging allowed.</returns>
    bool CanWrite();

    /// <summary>
    /// A method that should be called in the <see cref="ILogger"/> before logging a message to the console.
    /// </summary>
    /// <returns>Is logging to the console allowed.</returns>
    bool CanWriteToConsole();

    /// <summary>
    /// A method that should be called in the <see cref="ILogger"/> before logging a message to the file stream.
    /// </summary>
    /// <returns>Is logging to the file stream allowed.</returns>
    bool CanWriteToFile();

    /// <summary>
    /// A method that should be called in the <see cref="ILogger"/> before logging a message to the debugger.
    /// </summary>
    /// <returns>Is logging to the debugger allowed.</returns>
    bool CanWriteToDebug();

    /// <summary>
    /// The method that <see cref="ILogger"/> should call.
    /// </summary>
    /// <returns>Is logging allowed.</returns>
    bool PreWrite(Message message);
}