using System;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Terrain;
using Pss.ExampleRover.Domain.Models.Vehicle;
using Pss.ExampleRover.Domain.Services.Navigation;

namespace Pss.ExampleRover.Domain.UnitTests.Services.Navigation
{
    [TestFixture]
    public class MoveRoverServiceTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<ICoordinateCalculator> _coordinateCalculatorMock;

        [SetUp]
        public void SetUp()
        {
            _coordinateCalculatorMock = new Mock<ICoordinateCalculator>();
        }

        [Test]
        public void Ctor_CoordinateCalculatorNull_ThrowsException()
        {
            Assert.That(() => new MoveRoverService(null),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("coordinateCalculator"));
        }

        [Test]
        public void Move_WhenRoverIsNull_ThrowsException()
        {
            Assert.That(() => GetDefaultSut().Move(null, _fixture.Create<MoveDirection>()),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("rover"));
        }

        [Test]
        public void Move_ForGivenRoverAndDirection_GetNextCoordinateFromService()
        {
            var sut = GetDefaultSut();
            var deployedRover = _fixture.Create<DeployedRover>();
            var moveDirection = _fixture.Create<MoveDirection>();
            var expectedCurrentCoordinate = deployedRover.CurrentCoordinate;

            sut.Move(deployedRover, moveDirection);

            _coordinateCalculatorMock.Verify(c => c.CalculateNextCoordinate(expectedCurrentCoordinate, deployedRover.CurrentHeading, moveDirection), Times.Once);
        }

        [Test]
        public void Move_WhenFlatCoordinateWouldBeOutOfBounds_MovesRoverToPlanetaryCoordinate()
        {
            var planetMaxGridLocation = _fixture.Create<uint>();
            
            var planet = new Planet("Test", new SphericalGrid(planetMaxGridLocation + 1, planetMaxGridLocation + 1), Array.Empty<Coordinate>());
            _coordinateCalculatorMock.Setup(c =>
                    c.CalculateNextCoordinate(It.IsAny<Coordinate>(), It.IsAny<Heading>(), It.IsAny<MoveDirection>()))
                .Returns(new Coordinate((int) (planetMaxGridLocation + 1), (int) (planetMaxGridLocation + 1)));
            var deployedRover = new DeployedRover(planet, _fixture.Create<Coordinate>(), _fixture.Create<Heading>());

            var sut = GetDefaultSut();
            sut.Move(deployedRover, _fixture.Create<MoveDirection>());

            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, deployedRover.CurrentCoordinate.X);
                Assert.AreEqual(0, deployedRover.CurrentCoordinate.Y);
            });
        }

        [Test]
        public void Move_WhenPlanetHasObstacle_ReturnsFailed()
        {
            var obstacleCoordinate = new Coordinate(0, 1);
            var planet = new Planet("Test", new SphericalGrid(2, 2), new[] {obstacleCoordinate});
            _coordinateCalculatorMock.Setup(c =>
                    c.CalculateNextCoordinate(It.IsAny<Coordinate>(), It.IsAny<Heading>(), It.IsAny<MoveDirection>()))
                .Returns(obstacleCoordinate);

            var sut = GetDefaultSut();
            var result = sut.Move(new DeployedRover(planet, _fixture.Create<Coordinate>(), _fixture.Create<Heading>()), _fixture.Create<MoveDirection>());

            Assert.AreEqual(NavigationResult.FailedDueToObstacle, result);
        }

        [Test]
        public void Move_WhenPlanetDoesNotHaveObstacle_ReturnsSuccess()
        {
            var planet = new Planet("Test", new SphericalGrid(2, 2), Array.Empty<Coordinate>());
            _coordinateCalculatorMock.Setup(c =>
                    c.CalculateNextCoordinate(It.IsAny<Coordinate>(), It.IsAny<Heading>(), It.IsAny<MoveDirection>()))
                .Returns(new Coordinate(1, 1));

            var sut = GetDefaultSut();
            var result = sut.Move(new DeployedRover(planet, _fixture.Create<Coordinate>(), _fixture.Create<Heading>()), _fixture.Create<MoveDirection>());

            Assert.AreEqual(NavigationResult.Success, result);
        }

        private MoveRoverService GetDefaultSut() => new MoveRoverService(_coordinateCalculatorMock.Object);
    }
}