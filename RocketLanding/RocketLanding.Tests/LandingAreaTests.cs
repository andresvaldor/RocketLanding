using Microsoft.VisualStudio.TestTools.UnitTesting;
using RocketLanding.Contracts;
using System;

namespace RocketLanding.Tests;

[TestClass]
public class LandingAreaTests
{
    [TestMethod]
    [DataRow((uint)0, (uint)1, (uint)1, (uint)1, (uint)1)]
    [DataRow((uint)0, (uint)1, (uint)1, (uint)1, (uint)2)]
    [DataRow((uint)3, (uint)3, (uint)1, (uint)1, (uint)2)]
    [DataRow((uint)3, (uint)3, (uint)0, (uint)0, (uint)3)]
    [DataRow((uint)4, (uint)4, (uint)2, (uint)2, (uint)2)]
    public void CheckLandingPosition_WithPositionOutsideLandingPlatform_ReturnsOutOfPlatformMessage(uint xCoordinate, uint yCoordinate, uint landingPlatformX, uint landingPlatformY, uint landingPlatformSize)
    {
        // Arrange
        LandingArea sut = new();
        sut.LandingPlatformSize = landingPlatformSize;
        sut.LandingPlatformPosition = (landingPlatformX, landingPlatformY);

        var rocketId = Guid.NewGuid();

        // Act
        var result = sut.CheckLandingPosition(rocketId, xCoordinate, yCoordinate);

        // Assert
        Assert.AreEqual(LandingMessageResult.OutOfPlatform, result, $"Expected message 'out of platform' for this input ({xCoordinate},{yCoordinate}");
    }

    [TestMethod]
    [DataRow((uint)0, (uint)0)]
    [DataRow((uint)0, (uint)1)]
    [DataRow((uint)1, (uint)2)]
    [DataRow((uint)2, (uint)0)]
    [DataRow((uint)1, (uint)1)]
    [DataRow((uint)4, (uint)4)]
    public void CheckLandingPosition_WithPositionSameAsPreviousRocket_ReturnsClash(uint xCoordinate, uint yCoordinate)
    {
        // Arrange
        LandingArea sut = new();
        sut.LandingPlatformSize = 5;
        sut.LandingAreaSize = 5;
        sut.LandingPlatformPosition = (0, 0);

        var firstRocketId = Guid.NewGuid();
        var secondRocketId = Guid.NewGuid();

        // Act
        var firstRocketResult = sut.CheckLandingPosition(firstRocketId, xCoordinate, yCoordinate);
        var secondRocketResult = sut.CheckLandingPosition(secondRocketId, xCoordinate, yCoordinate);

        // Assert
        Assert.AreEqual(LandingMessageResult.OkForLanding, firstRocketResult, $"Expected message 'ok for landing' for input ({xCoordinate},{yCoordinate}) for first rocket");
        Assert.AreEqual(LandingMessageResult.Clash, secondRocketResult, $"Expected message 'clash' for input ({xCoordinate},{yCoordinate}) for second rocket");
    }

    [TestMethod]
    [DataRow((uint)1, (uint)1, (uint)2, (uint)2)]
    [DataRow((uint)2, (uint)1, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)1, (uint)2, (uint)2)]
    [DataRow((uint)1, (uint)2, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)2, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)3, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)3, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)3, (uint)2, (uint)2)]
    public void CheckLandingPosition_WithPositionNextToPositionPreviouslyChecked_ReturnsClash(uint xCoordinate, uint yCoordinate, uint prevCheckedXCoordinate, uint prevCheckedYCoordinate)
    {
        // Arrange
        LandingArea sut = new();
        sut.LandingPlatformSize = 5;
        sut.LandingAreaSize = 5;
        sut.LandingPlatformPosition = (0, 0);

        var firstRocketId = Guid.NewGuid();
        var secondRocketId = Guid.NewGuid();

        // Act
        var firstRocketResult = sut.CheckLandingPosition(firstRocketId, prevCheckedXCoordinate, prevCheckedYCoordinate);
        var secondRocketResult = sut.CheckLandingPosition(secondRocketId, xCoordinate, yCoordinate);

        // Assert
        Assert.AreEqual(LandingMessageResult.OkForLanding, firstRocketResult, $"Expected message 'ok for landing' for input ({prevCheckedXCoordinate},{prevCheckedYCoordinate}) for first rocket");
        Assert.AreEqual(LandingMessageResult.Clash, secondRocketResult, $"Expected message 'clash' for input ({xCoordinate},{yCoordinate}) for second rocket");
    }

    [TestMethod]
    [DataRow((uint)1, (uint)1, (uint)2, (uint)2)]
    [DataRow((uint)2, (uint)1, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)1, (uint)2, (uint)2)]
    [DataRow((uint)1, (uint)2, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)2, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)3, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)3, (uint)2, (uint)2)]
    [DataRow((uint)3, (uint)3, (uint)2, (uint)2)]
    public void CheckLandingPosition_WithPositionNextToPositionPreviouslyCheckedBySameRocket_ReturnsOkForLanding(uint xCoordinate, uint yCoordinate, uint prevCheckedXCoordinate, uint prevCheckedYCoordinate)
    {
        // Arrange
        LandingArea sut = new();
        sut.LandingPlatformSize = 5;
        sut.LandingAreaSize = 5;
        sut.LandingPlatformPosition = (0, 0);

        var rocketId = Guid.NewGuid();

        // Act
        var firstRocketResult = sut.CheckLandingPosition(rocketId, prevCheckedXCoordinate, prevCheckedYCoordinate);
        var secondRocketResult = sut.CheckLandingPosition(rocketId, xCoordinate, yCoordinate);

        // Assert
        Assert.AreEqual(LandingMessageResult.OkForLanding, firstRocketResult, $"Expected message 'ok for landing' for input ({prevCheckedXCoordinate},{prevCheckedYCoordinate}) for first rocket");
        Assert.AreEqual(LandingMessageResult.OkForLanding, secondRocketResult, $"Expected message 'clash' for input ({xCoordinate},{yCoordinate}) for second rocket");
    }

    [TestMethod]
    [DataRow((uint)0, (uint)0)]
    [DataRow((uint)1, (uint)0)]
    [DataRow((uint)2, (uint)0)]
    [DataRow((uint)3, (uint)0)]
    [DataRow((uint)4, (uint)0)]
    [DataRow((uint)1, (uint)4)]
    [DataRow((uint)0, (uint)4)]
    [DataRow((uint)1, (uint)4)]
    public void CheckLandingPosition_WithCorrectInputPosition_ReturnsOkForLanding(uint xCoordinate, uint yCoordinate)
    {
        // Arrange
        LandingArea sut = new();
        sut.LandingPlatformSize = 5;
        sut.LandingAreaSize = 5;
        sut.LandingPlatformPosition = (0, 0);

        var firstRocketId = Guid.NewGuid();
        var secondRocketId = Guid.NewGuid();
        var sutRocketId = Guid.NewGuid();

        // Act
        var firstRocketResult = sut.CheckLandingPosition(firstRocketId, 1, 2);
        var secondRocketResult = sut.CheckLandingPosition(secondRocketId, 3, 3);
        var result = sut.CheckLandingPosition(sutRocketId, xCoordinate, yCoordinate);

        // Assert
        Assert.AreEqual(LandingMessageResult.OkForLanding, firstRocketResult, "Expected message 'ok for landing' for input (1,2) for first rocket");
        Assert.AreEqual(LandingMessageResult.OkForLanding, secondRocketResult, "Expected message 'ok for landing' for input (1,2) for first rocket");
        Assert.AreEqual(LandingMessageResult.OkForLanding, result, $"Expected message 'ok for landing' for input ({xCoordinate},{yCoordinate}) for tested rocket");
    }

    [TestMethod]
    public void CheckLandingPosition_WithPreviousCheckedRocketIdEqualToRocketId_ReturnsOkForLanding()
    {
        // Arrange
        LandingArea sut = new();
        sut.LandingPlatformSize = 4;
        sut.LandingAreaSize = 5;
        sut.LandingPlatformPosition = (0, 1);

        uint landingPositionX = 1;
        uint landingPositionY = 1;
        var rocketId = Guid.NewGuid();

        // Act
        var firstCheck = sut.CheckLandingPosition(rocketId, landingPositionX, landingPositionY);
        var secondCheck = sut.CheckLandingPosition(rocketId, landingPositionX, landingPositionY);

        // Assert
        Assert.AreEqual(LandingMessageResult.OkForLanding, firstCheck, "Expected message 'ok for landing' for first check");
        Assert.AreEqual(LandingMessageResult.OkForLanding, secondCheck, "Expected message 'ok for landing' for second check");
    }

    [TestMethod]
    public void CheckLandingPosition_WithRocketChangeCheckingPosition_PositionIsFreeForOtherRocketToLand()
    {
        // Arrange
        LandingArea sut = new();
        sut.LandingPlatformSize = 4;
        sut.LandingAreaSize = 5;
        sut.LandingPlatformPosition = (0, 1);

        var firstRocketId = Guid.NewGuid();
        var secondRocketId = Guid.NewGuid();

        // Act
        var firstCheck = sut.CheckLandingPosition(firstRocketId, 1, 1);
        var secondCheck = sut.CheckLandingPosition(firstRocketId, 3, 3);
        var thirdCheck = sut.CheckLandingPosition(secondRocketId, 1, 1);

        // Assert
        Assert.AreEqual(LandingMessageResult.OkForLanding, firstCheck, "Expected message 'ok for landing' for first check");
        Assert.AreEqual(LandingMessageResult.OkForLanding, secondCheck, "Expected message 'ok for landing' for second check");
        Assert.AreEqual(LandingMessageResult.OkForLanding, thirdCheck, "Expected message 'ok for landing' for third check");
    }

    [TestMethod]
    public void CheckLandingPosition_WithPositionAlreadyCheckedBySameRocket_ReturnsOkForLanding()
    {
        // Arrange
        LandingArea sut = new();
        sut.LandingPlatformSize = 4;
        sut.LandingAreaSize = 5;
        sut.LandingPlatformPosition = (0, 1);

        var firstRocketId = Guid.NewGuid();
        var secondRocketId = Guid.NewGuid();

        // Act
        var firstCheck = sut.CheckLandingPosition(firstRocketId, 1, 1);
        var secondCheck = sut.CheckLandingPosition(secondRocketId, 3, 3);
        var thirdCheck = sut.CheckLandingPosition(firstRocketId, 1, 1);

        // Assert
        Assert.AreEqual(LandingMessageResult.OkForLanding, firstCheck, "Expected message 'ok for landing' for first check");
        Assert.AreEqual(LandingMessageResult.OkForLanding, secondCheck, "Expected message 'ok for landing' for second check");
        Assert.AreEqual(LandingMessageResult.OkForLanding, thirdCheck, "Expected message 'ok for landing' for third check");
    }
}