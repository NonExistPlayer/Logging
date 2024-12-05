namespace NonExistPlayer.Logging;

public interface IDebugLogger
{
    /// <summary>
    /// Should <see cref="Logger"/> output a message to the debugger.
    /// Defaults to <see langword="false"/>.
    /// </summary>
    bool WriteToDebug { get; set; }
}