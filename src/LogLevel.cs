using static System.ConsoleColor;

namespace NonExistPlayer.Logging;

/// <summary>
/// Built-in <see cref="ILogLevel"/> implementation.
/// Represents the standard concept of logging level.
/// This class cannot be inherited.
/// </summary>
public sealed class LogLevel : ILogLevel, IEnumAsClass
{
    public LogLevel(ushort level)
    {
        if (level > MaxLevel) throw new ArgumentOutOfRangeException(nameof(level));
        Value = level;
    }

    public static LogLevel Info { get; } = new(0);
    public static LogLevel Warning { get; } = new(1);
    public static LogLevel Error { get; } = new(2);

    public const ushort MaxLevel = 2;

    public static ushort Max => MaxLevel;
    public static ushort Min => 0;

    public static Dictionary<ushort, string> LevelNamePairs { get; } = new()
    {
        { 0, "Info" },
        { 1, "Warning" },
        { 2, "Error" },
    };

    public static Dictionary<ushort, ConsoleColor> LevelColorPairs { get; } = new()
    {
        { 0, Gray },
        { 1, Yellow },
        { 2, Red },
    };

    public ushort Value { get; }

    public ConsoleColor GetColor() => LevelColorPairs[Value];

    public bool IsError() => Value == 2;

    public override string ToString() => LevelNamePairs[Value];

    public static implicit operator LogLevel(ushort level) => new(level);
}