namespace RocketLanding;

internal class CheckedPositionCache
{
    internal Dictionary<(uint x, uint y), Guid> CheckedPositionsByPosition { get; set; } = new();

    internal Dictionary<Guid, (uint x, uint y)> CheckedPositionsByRocket { get; set; } = new();

    internal ((uint x, uint y) position, Guid rocketId)? LatestCheckedPosition { get; set; }
}