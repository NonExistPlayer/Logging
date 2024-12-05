using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NonExistPlayer.Logging.Tests;

[TestClass]
public sealed class LoggerTest
{
    [TestMethod]
    public void BuiltInLogger_Test()
    {
        Logger logger = new()
        {
            WriteToDebug = true
        };
        
        logger.Log("Hello from Logger tests!");

        Assert.IsFalse(logger.Messages.Length == 0);

        logger.OutputFormat = "%time %thread %type %caller %mes";

        logger.Log("New output format!");
    }

    [TestMethod]
    public void BuiltInLogger1_Test() // Logger`1
    {
        Logger<CustomLogLevel> logger = new(new(0))
        {
            WriteToDebug = true,
            WarningLogLevel = new(2),
            ErrorLogLevel = new(3)
        };

        logger.Log("Hello from Logger`1 tests!");

        logger.Warn("Warning from Logger`1 tests!");

        logger.Error("Error from Logger`1 tests!");
    }
}