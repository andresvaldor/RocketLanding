using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RocketLanding.Tests")]

namespace RocketLanding.Contracts;

internal static class LandingMessageResult
{
    public static readonly string OkForLanding = "ok for landing";
    public static readonly string OutOfPlatform = "out of platform";
    public static readonly string Clash = "clash";
}