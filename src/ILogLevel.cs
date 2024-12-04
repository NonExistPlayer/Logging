namespace NonExistPlayer.Logging;

public interface ILogLevel : IEnumAsClass
{
    ConsoleColor GetColor();

    bool IsError();

    static abstract ILogLevel FromUInt16(ushort value);
}