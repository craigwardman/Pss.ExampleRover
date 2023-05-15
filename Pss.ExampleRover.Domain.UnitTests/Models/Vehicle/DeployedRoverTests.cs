using AutoFixture;
using Moq;
using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Terrain;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Domain.UnitTests.Models.Vehicle
{
    [TestFixture]
    public class DeployedRoverTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Ctor_WhenCalled_CorrectlyInitializes()
        {
            var planet = _fixture.Create<Planet>();
            var coordinate = _fixture.Create<Coordinate>();
            var heading = _fixture.Create<Heading>();

            var sut = new DeployedRover(planet, coordinate, heading);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(planet, sut.Planet);
                Assert.AreEqual(coordinate, sut.CurrentCoordinate);
                Assert.AreEqual(heading, sut.CurrentHeading);
            });
        }

        [Test]
        public void SetHeading_WhenCalled_UpdatesHeading()
        {
            var heading = _fixture.Create<Heading>();

            var sut = _fixture.Create<DeployedRover>();
            sut.SetHeading(heading);

            Assert.AreEqual(heading, sut.CurrentHeading);
        }

        [Test]
        public void SetCoordinate_WhenCalled_UpdatesCoordinate()
        {
            var coordinate = _fixture.Create<Coordinate>();

            var sut = _fixture.Create<DeployedRover>();
            sut.SetCoordinate(coordinate);

            Assert.AreEqual(coordinate, sut.CurrentCoordinate);
        }

        [Test]
        public void Navigate_WhenCommandIsNull_ThrowsException()
        {
            var sut = _fixture.Create<DeployedRover>();

            Assert.That(() => sut.Navigate(null), 
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("command"));
        }

        [Test]
        public void Navigate_WhenCalled_InvokesCommand()
        {
            var mockCommand = new Mock<NavigationCommand>();

            var sut = _fixture.Create<DeployedRover>();
            sut.Navigate(mockCommand.Object);

            mockCommand.Verify(n => n.Invoke(sut), Times.Once);
        }
    }
}