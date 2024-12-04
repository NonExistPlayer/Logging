using static System.ConsoleColor;

namespace NonExistPlayer.Logging;

public sealed class LogLevel : ILogLevel
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

    public static LogLevel? FromEnum(object enumval, Type enumtype)
    {
        if (enumval is null || enumtype is null) return null;
        
        string? name = Enum.GetName(enumtype, enumval);

        if (name is null) return null;

        Type self = typeof(LogLevel);

        var property = self.GetProperties().Where(p => p.PropertyType == self).FirstOrDefault(p => p.Name == name);

        if (property is null) return null;
        return property.GetValue(null) as LogLevel;
    }

    public static ILogLevel FromUInt16(ushort level) => new LogLevel(level);

    public static implicit operator LogLevel(ushort level) => new(level);
}