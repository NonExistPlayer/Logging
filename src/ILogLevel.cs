namespace NonExistPlayer.Logging;

public interface ILogLevel : IEnumAsClass
{
    string ToString();

    ConsoleColor GetColor();

    bool IsError();

    static abstract ILogLevel FromUInt16(ushort value);
}