namespace NonExistPlayer.Logging;

public interface ILogLevel
{
    ushort Level { get; }

    string ToString();

    ConsoleColor GetColor();

    bool IsError();

    static abstract ILogLevel FromUInt16(ushort value);
}

public interface ILogLevel<self>