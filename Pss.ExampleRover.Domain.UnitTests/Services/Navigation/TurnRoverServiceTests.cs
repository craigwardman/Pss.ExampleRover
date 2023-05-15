using AutoFixture;
using Moq;
using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Vehicle;
using Pss.ExampleRover.Domain.Services.Navigation;

namespace Pss.ExampleRover.Domain.UnitTests.Services.Navigation
{
    [TestFixture]
    public class TurnRoverServiceTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IHeadingCalculator> _headingCalculatorMock;

        [SetUp]
        public void SetUp()
        {
            _headingCalculatorMock = new Mock<IHeadingCalculator>();
        }

        [Test]
        public void Ctor_CoordinateCalculatorNull_ThrowsException()
        {
            Assert.That(() => new TurnRoverService(null),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("headingCalculator"));
        }

        [Test]
        public void Move_WhenRoverIsNull_ThrowsException()
        {
            Assert.That(() => GetDefaultSut().Turn(null, _fixture.Create<TurnDirection>()),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("rover"));
        }

        [Test]
        public void Turn_WhenCalled_GetsNewHeadingFromService()
        {
            var rover = _fixture.Create<DeployedRover>();
            var expectedInitialHeading = rover.CurrentHeading;
            var turnDirection = _fixture.Create<TurnDirection>();

            var sut = GetDefaultSut();
            sut.Turn(rover, turnDirection);

            _headingCalculatorMock.Verify(c => c.GetAdjustedHeading(expectedInitialHeading, turnDirection), Times.Once);
        }

        [Test]
        public void Turn_WhenCalled_SetsRoverHeading()
        {
            var heading = _fixture.Create<Heading>();
            _headingCalculatorMock.Setup(c => c.GetAdjustedHeading(It.IsAny<Heading>(), It.IsAny<TurnDirection>()))
                .Returns(heading);
            var rover = _fixture.Create<DeployedRover>();
            
            var sut = GetDefaultSut();
            sut.Turn(rover, _fixture.Create<TurnDirection>());

            Assert.AreEqual(heading, rover.CurrentHeading);
        }

        private TurnRoverService GetDefaultSut() => new TurnRoverService(_headingCalculatorMock.Object);
    }
}