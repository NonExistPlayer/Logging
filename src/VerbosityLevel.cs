namespace NonExistPlayer.Logging;

/// <summary>
/// Built-in <see cref="IVerbosityLevel"/> implementation.
/// A class built into the <see cref="Logging"/> library that represents the verbosity level.
/// This class cannot be inherited.
/// </summary>
public sealed class VerbosityLevel : IVerbosityLevel, IEnumAsClass
{
    public VerbosityLevel(ushort level)
    {
        if (level > MaxLevel) throw new ArgumentOutOfRangeException(nameof(level));
        Value = level;
    }

    public const ushort MaxLevel = 3;
    public static ushort Max => MaxLevel;
    public static ushort Min => 0;

    public static VerbosityLevel Quiet { get; } = new(0);
    public static VerbosityLevel Minimal { get; } = new(1);
    public static VerbosityLevel Middle { get; } = new(2);
    public static VerbosityLevel Maximal { get; } = new(3);

    public ushort Value { get; }

    public bool CanWrite() => Value > 0;
    public bool CanWriteToConsole() => Value > 1;
    public bool CanWriteToFile() => Value > 0;
    public bool CanWriteToDebug() => Value > 2;

    public bool PreWrite(Message message) => true;
}