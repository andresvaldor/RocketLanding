using Microsoft.VisualStudio.TestTools.UnitTesting;
using RocketLanding.Exceptions;
using RocketLanding.Builders;

namespace RocketLanding.Tests;

[TestClass]
public class LandingAreaBuilderTests
{
    [TestMethod]
    public void Build_WithLandingAreaSizeEqualToZero_ThrowsZeroAreaSizeException()
    {
        // Arrange
        LandingAreaBuilder sut = new();

        // Act & Assert
        Assert.ThrowsException<ZeroAreaSizeException>(() => sut.WithLandingAreaSize(0).Build());
    }

    [TestMethod]
    [DataRow((uint)2, (uint)1)]
    [DataRow((uint)10, (uint)1)]
    [DataRow((uint)10, (uint)5)]
    [DataRow((uint)10, (uint)9)]
    [DataRow((uint)5, (uint)1)]
    [DataRow((uint)5, (uint)2)]
    [DataRow((uint)5, (uint)3)]
    [DataRow((uint)5, (uint)4)]
    public void Build_WithLandingPlatformLargerThanLandingArea_ThrowsPlatformSizeOverflowException(uint landingPlatformSize, uint landingAreaSize)
    {
        // Arrange
        LandingAreaBuilder sut = new();

        // Act & Assert
        Assert.ThrowsException<PlatformSizeOverflowException>(
                                                                () => sut.WithLandingPlatformSize(landingPlatformSize)
                                                                            .WithLandingAreaSize(landingAreaSize)
                                                                            .Build()
                                                             );
    }

    [TestMethod]
    [DataRow((uint)1)]
    [DataRow((uint)2)]
    [DataRow((uint)4)]
    [DataRow((uint)8)]
    [DataRow((uint)10)]
    [DataRow((uint)20)]
    [DataRow((uint)25)]
    [DataRow((uint)100)]
    [DataRow((uint)10_000)]
    [DataRow((uint)10_000_000)]
    [DataRow(4_294_967_295)]
    public void Build_WithCorrectLandingAreaInputSize_SetsLandingAreaSize(uint setLandingAreaSize)
    {
        // Arrange
        LandingAreaBuilder sut = new();

        // Act
        var result = sut.WithLandingAreaSize(setLandingAreaSize)
                        .WithLandingPlatformSize((uint)1)
                        .Build();

        // Assert
        Assert.AreEqual(setLandingAreaSize, result.LandingAreaSize, "The sut landing area size is not equal to the input area size");
    }

    [TestMethod]
    [DataRow((uint)1, (uint)1, (uint)1, (uint)1)]
    [DataRow((uint)1, (uint)1, (uint)2, (uint)2)]
    [DataRow((uint)0, (uint)1, (uint)2, (uint)2)]
    [DataRow((uint)1, (uint)0, (uint)2, (uint)2)]
    [DataRow((uint)2, (uint)2, (uint)4, (uint)5)]
    [DataRow((uint)5, (uint)5, (uint)2, (uint)6)]
    [DataRow((uint)6, (uint)6, (uint)1, (uint)1)]
    public void Build_WithLandingPlatformPositionOutOfLandingArea_ThrowsPositionOutOfRangeException(uint xCoordinate, uint yCoordinate, uint platformSize, uint landingAreaSize)
    {
        // Arrange
        LandingAreaBuilder sut = new();

        // Act & Assert
        Assert.ThrowsException<PositionOutOfRangeException>(
                                                                () => sut.WithLandingPlatformPosition(xCoordinate, yCoordinate)
                                                                         .WithLandingAreaSize(landingAreaSize)
                                                                         .WithLandingPlatformSize(platformSize)
                                                                         .Build()
                                                           );
    }

    [TestMethod]
    [DataRow((uint)0, (uint)0)]
    [DataRow((uint)1, (uint)0)]
    [DataRow((uint)2, (uint)0)]
    [DataRow((uint)3, (uint)0)]
    [DataRow((uint)4, (uint)0)]
    [DataRow((uint)0, (uint)1)]
    [DataRow((uint)0, (uint)2)]
    [DataRow((uint)0, (uint)3)]
    [DataRow((uint)1, (uint)1)]
    [DataRow((uint)4, (uint)4)]
    [DataRow((uint)3, (uint)3)]
    [DataRow((uint)2, (uint)1)]
    [DataRow((uint)0, (uint)0)]
    public void Build_WithCorrectLandingPlatformInputPosition_SetsLandingAreaPosition(uint xCoordinate, uint yCoordinate)
    {
        // Arrange
        LandingAreaBuilder sut = new();

        // Act
        var result = sut.WithLandingAreaSize(5)
                        .WithLandingPlatformSize(1)
                        .WithLandingPlatformPosition(xCoordinate, yCoordinate)
                        .Build();

        // Assert
        Assert.AreEqual(xCoordinate, result.LandingPlatformPosition.x, "The sut landing position x coordinate is not equal to the input xCoordinate");
        Assert.AreEqual(yCoordinate, result.LandingPlatformPosition.y, "The sut landing position x coordinate is not equal to the input xCoordinate");
    }

    [TestMethod]
    public void Build_WithLandingPlatformSizeEqualToZero_ThrowsZeroAreaSizeException()
    {
        // Arrange
        LandingAreaBuilder sut = new();

        // Act & Assert
        Assert.ThrowsException<ZeroAreaSizeException>(() => sut.WithLandingPlatformSize(0).Build());
    }

    [TestMethod]
    [DataRow((uint)1)]
    [DataRow((uint)2)]
    [DataRow((uint)4)]
    [DataRow((uint)8)]
    [DataRow((uint)10)]
    [DataRow((uint)20)]
    [DataRow((uint)25)]
    [DataRow((uint)100)]
    [DataRow((uint)10_000)]
    [DataRow((uint)10_000_000)]
    [DataRow(4_294_967_295)]
    public void Build_WithLandingPlatformSizeCorrectInputSize_SetsLandingAreaSize(uint setLandingPlatformSize)
    {
        // Arrange
        LandingAreaBuilder sut = new();

        // Act
        var result = sut.WithLandingAreaSize(4_294_967_295).WithLandingPlatformSize(setLandingPlatformSize).Build();

        // Assert
        Assert.AreEqual(setLandingPlatformSize, result.LandingPlatformSize, "The sut landing platform size is not equal to the input area size");
    }
}