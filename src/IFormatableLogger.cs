namespace NonExistPlayer.Logging;

public interface IFormatableLogger : ILogger
{
    /// <summary>
    /// The format that will be used to format the output message.<br/>
    /// - %time = Current time.<br/>
    /// - %thread = Current thread.<br/>
    /// - %type, %Type, %TYPE = The result of <see cref="ILogLevel.ToString()"/> is case-sensitive, depending on the case of this word.<br/>
    /// - %namespace = Caller namespace.<br/>
    /// - %class = Caller class name.<br/>
    /// - %method = Caller method name.<br/>
    /// - %mes = Unformatted message.
    /// </summary>
    string OutputFormat { get; set; }
}