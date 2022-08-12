using System.Runtime.Serialization;

namespace RocketLanding.Exceptions;

[Serializable]
public class PlatformSizeOverflowException : Exception
{
    private const string innerMessage = "The landing platform cannot be larger than the landing area";

    public PlatformSizeOverflowException() : base(innerMessage)
    {
    }

    public PlatformSizeOverflowException(string message) : base($"{innerMessage}. {message}")
    {
    }

    public PlatformSizeOverflowException(string message, Exception inner) : base($"{innerMessage}. {message}", inner)
    {
    }

    protected PlatformSizeOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}