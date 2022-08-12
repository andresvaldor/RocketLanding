namespace RocketLanding.Builders;

public interface ILandingAreaBuilder
{
    ILandingAreaBuilder WithLandingAreaSize(uint size);

    ILandingAreaBuilder WithLandingPlatformSize(uint size);

    ILandingAreaBuilder WithLandingPlatformPosition(uint x, uint y);

    LandingArea Build();
}