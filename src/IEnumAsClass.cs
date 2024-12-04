namespace NonExistPlayer.Logging;

/// <summary>
/// Represents an enumeration as a class.<br/>
/// This allows you not to be limited to certain enumerations.
/// And allows you to make your own extended enumerations.
/// </summary>
public interface IEnumAsClass
{
    /// <summary>
    /// The meaning of enumeration.
    /// </summary>
    ushort Value { get; }

    string ToString();

    /// <summary>
    /// The maximum value allowed for this type.
    /// </summary>
    static abstract ushort Max { get; }
    /// <summary>
    /// The minimum value allowed for this type.
    /// </summary>
    static abstract ushort Min { get; }
}