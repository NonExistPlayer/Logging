namespace NonExistPlayer.Logging;

public interface IVerbosityLevel : IEnumAsClass
{
    bool CanWrite();

    bool CanWriteToConsole();

    bool CanWriteToFile();

    bool CanWriteToDebug();
    
    bool PreWrite(Message message);
}