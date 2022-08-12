using System.Runtime.Serialization;

namespace RocketLanding.Exceptions;

[Serializable]
public class ZeroAreaSizeException : Exception
{
    private const string innerMessage = "The area cannot be set to a value less than or equal to zero";

    public ZeroAreaSizeException() : base(innerMessage)
    {
    }

    public ZeroAreaSizeException(string message) : base($"{message} - {innerMessage}")
    {
    }

    public ZeroAreaSizeException(string message, Exception inner) : base($" {message} - {innerMessage}", inner)
    {
    }

    protected ZeroAreaSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}