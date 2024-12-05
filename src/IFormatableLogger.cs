namespace NonExistPlayer.Logging;

public interface IFormatableLogger : ILogger
{
    /// <summary>
    /// The format that will be used to format the output message.<br/>
    /// - %time = Current time.<br/>
    /// - %thread = Current thread.<br/>
    /// - %type, %Type, %TYPE = The result of <see cref="ILogLevel.ToString()"/> is case-sensitive, depending on the case of this word.<br/>
    /// - %caller = Caller.<br/>
    /// - %mes = Unformatted message.
    /// </summary>
    string OutputFormat { get; set; }
}