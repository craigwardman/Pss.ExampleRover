using AutoFixture;
using Moq;
using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Vehicle;
using Pss.ExampleRover.Domain.Services.Navigation;

namespace Pss.ExampleRover.Domain.UnitTests.Models.Vehicle
{
    [TestFixture]
    public class TurnCommandTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Ctor_TurnRoverServiceNull_ThrowsException()
        {
            Assert.That(() => new TurnCommand(_fixture.Create<TurnDirection>(), null),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("turnRoverService"));
        }

        [Test]
        public void Invoke_WhenCalled_UsesTurnService()
        {
            var turnServiceMock = new Mock<ITurnRoverService>();
            var direction = _fixture.Create<TurnDirection>();
            var deployedRover = _fixture.Create<DeployedRover>();

            var sut = new TurnCommand(direction, turnServiceMock.Object);
            sut.Invoke(deployedRover);

            turnServiceMock.Verify(m => m.Turn(deployedRover, direction));
        }
    }
}