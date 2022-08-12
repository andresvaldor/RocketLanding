namespace RocketLanding.Contracts;

public interface IRocketLanding
{
    string CheckLandingPosition(Guid rocketId, uint x, uint y);
}