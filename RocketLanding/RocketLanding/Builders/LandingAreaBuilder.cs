using RocketLanding.Exceptions;

namespace RocketLanding.Builders;

/// <summary>
/// Builder class used to build a LandingArea object. It allows setting its landing area size, platform size and platform position
/// </summary>
public class LandingAreaBuilder : ILandingAreaBuilder
{
    private uint? landingAreaSize;
    private uint? landingPlatformSize;
    private (uint x, uint y)? landingPlatformPosition;

    public LandingAreaBuilder()
    {
        SetDefault();
    }

    private void SetDefault()
    {
        landingAreaSize = null;
        landingPlatformSize = null;
        landingPlatformPosition = null;
    }

    public LandingArea Build()
    {
        landingAreaSize ??= uint.MaxValue;
        landingPlatformSize ??= 1;
        landingPlatformPosition ??= (0, 0);

        CheckZeroArea();
        CheckLandingPlatformSize();
        CheckLandingPlatformPosition();

        LandingArea landingArea = new();
        landingArea.LandingAreaSize = landingAreaSize ?? landingArea.LandingAreaSize;
        landingArea.LandingPlatformPosition = landingPlatformPosition ?? landingArea.LandingPlatformPosition;
        landingArea.LandingPlatformSize = landingPlatformSize ?? landingArea.LandingPlatformSize;

        SetDefault();

        return landingArea;
    }

    public ILandingAreaBuilder WithLandingAreaSize(uint size)
    {
        landingAreaSize = size;

        return this;
    }

    public ILandingAreaBuilder WithLandingPlatformSize(uint size)
    {
        landingPlatformSize = size;

        return this;
    }

    public ILandingAreaBuilder WithLandingPlatformPosition(uint x, uint y)
    {
        landingPlatformPosition = (x, y);

        return this;
    }

    private void CheckLandingPlatformSize()
    {
        if (landingPlatformSize > landingAreaSize)
        {
            throw new PlatformSizeOverflowException();
        }
    }

    private void CheckLandingPlatformPosition()
    {
        if (landingPlatformPosition is null)
        {
            return;
        }

        if ((landingPlatformPosition.Value.x + landingPlatformSize > landingAreaSize) || (landingPlatformPosition.Value.y + landingPlatformSize > landingAreaSize))
        {
            throw new PositionOutOfRangeException();
        }
    }

    private void CheckZeroArea()
    {
        if (landingAreaSize == 0)
        {
            throw new ZeroAreaSizeException(nameof(landingAreaSize));
        }

        if (landingPlatformSize == 0)
        {
            throw new ZeroAreaSizeException(nameof(landingPlatformSize));
        }
    }
}