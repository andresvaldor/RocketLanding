using System.Runtime.Serialization;

namespace RocketLanding.Exceptions;

[Serializable]
public class PositionOutOfRangeException : Exception
{
    private const string innerMessage = "The landing position is outside the landing area";

    public PositionOutOfRangeException() : base(innerMessage)
    {
    }

    public PositionOutOfRangeException(string message) : base($"{innerMessage}. {message}")
    {
    }

    public PositionOutOfRangeException(string message, Exception inner) : base($"{innerMessage}. {message}", inner)
    {
    }

    protected PositionOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}