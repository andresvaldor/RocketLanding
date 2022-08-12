using RocketLanding.Contracts;

namespace RocketLanding;

/// <summary>
/// Provides methods to determine if a rocket can land on a platform.
/// </summary>
public class LandingArea : IRocketLanding
{
    public uint LandingAreaSize { get; internal set; } = uint.MaxValue;
    public (uint x, uint y) LandingPlatformPosition { get; internal set; } = (0, 0);
    public uint LandingPlatformSize { get; internal set; } = 1;

    private readonly object checkedPositionsLock = new();
    private readonly CheckedPositionCache checkedPositions = new();

    /// <summary>
    /// Check if the rocket landing position is safe given x and y coordinates
    /// </summary>
    /// <param name="rocketId">Unique identifier for the rocket</param>
    /// <param name="x">X coordinate at the 2d landing area</param>
    /// <param name="y">Y coordinate at the 2d landing area</param>
    /// <returns>
    /// 'ok for landing' -> The position is free to land
    /// 'clash' -> The previous rocket has checked this position or is a danger position (any rocket has checked a position near it)
    /// 'out of platform' -> The requested position is outside the landing platform area
    /// </returns>
    public string CheckLandingPosition(Guid rocketId, uint x, uint y)
    {
        lock (checkedPositionsLock)
        {
            if (checkedPositions.LatestCheckedPosition?.position == (x, y) && checkedPositions.LatestCheckedPosition?.rocketId == rocketId)
            {
                return LandingMessageResult.OkForLanding;
            }

            if (checkedPositions.CheckedPositionsByRocket.TryGetValue(rocketId, out var position))
            {
                if (position.x == x && position.y == y)
                {
                    checkedPositions.LatestCheckedPosition = ((x, y), rocketId);
                    return LandingMessageResult.OkForLanding;
                }
            }

            if (!IsPositionInsidePlatform(x, y))
            {
                return LandingMessageResult.OutOfPlatform;
            }

            if (IsPositionCheckedByLatestRocket(rocketId, x, y))
            {
                return LandingMessageResult.Clash;
            }

            if (IsPositionInDangerZone(rocketId, x, y))
            {
                return LandingMessageResult.Clash;
            }

            checkedPositions.LatestCheckedPosition = ((x, y), rocketId);

            AddCheckedPositionToCache(rocketId, x, y);

            return LandingMessageResult.OkForLanding;
        }
    }

    private void AddCheckedPositionToCache(Guid rocketId, uint x, uint y)
    {
        var isRocketPreviouslyChecked = checkedPositions.CheckedPositionsByRocket.TryGetValue(rocketId, out var previousRocketPosition);

        if (isRocketPreviouslyChecked)
        {
            checkedPositions.CheckedPositionsByPosition.Remove(previousRocketPosition);
            checkedPositions.CheckedPositionsByRocket[rocketId] = (x, y);
        }
        else
        {
            checkedPositions.CheckedPositionsByRocket.Add(rocketId, (x, y));
        }

        checkedPositions.CheckedPositionsByPosition.Add((x, y), rocketId);
    }

    private bool IsPositionInDangerZone(Guid rocketId, uint x, uint y)
    {
        return IsDangerPosition(x + 1, y, rocketId)
               || IsDangerPosition(x + 1, y - 1, rocketId)
               || IsDangerPosition(x + 1, y + 1, rocketId)
               || IsDangerPosition(x, y + 1, rocketId)
               || IsDangerPosition(x, y - 1, rocketId)
               || IsDangerPosition(x - 1, y, rocketId)
               || IsDangerPosition(x - 1, y - 1, rocketId)
               || IsDangerPosition(x - 1, y + 1, rocketId);
    }

    private bool IsDangerPosition(uint x, uint y, Guid rocketId)
    {
        if (checkedPositions.CheckedPositionsByPosition.TryGetValue((x, y), out var checkedRocketId))
        {
            return (checkedRocketId != rocketId);
        }

        return false;
    }

    private bool IsPositionCheckedByLatestRocket(Guid rocketId, uint x, uint y)
    {
        var latestCheckedPosition = checkedPositions.LatestCheckedPosition;

        if (latestCheckedPosition is null)
        {
            return false;
        }

        return (latestCheckedPosition.Value.rocketId != rocketId && latestCheckedPosition.Value.position.x == x && latestCheckedPosition.Value.position.y == y);
    }

    private bool IsPositionInsidePlatform(uint x, uint y)
    {
        return (
                   x >= LandingPlatformPosition.x
                && y >= LandingPlatformPosition.y
                && x < LandingPlatformPosition.x + LandingPlatformSize
                && y < LandingPlatformPosition.y + LandingPlatformSize
               );
    }
}