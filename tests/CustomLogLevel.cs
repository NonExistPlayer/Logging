namespace NonExistPlayer.Logging.Tests;

internal class CustomLogLevel(ushort value) : ILogLevel
{
    public static ushort Max => 5;
    public static ushort Min => 0;

    public ushort Value { get; } = value;

    public bool IsError() => Value is 3 or 5;

    public ConsoleColor GetColor() => ConsoleColor.Gray;

    public override string ToString()
    {
        return Value switch
        {
            0 => "Info",
            1 => "Debug",
            2 => "Warning",
            3 => "Error",
            4 => "Important",
            5 => "Exception",
            _ => "<unknown>"
        };
    }
}