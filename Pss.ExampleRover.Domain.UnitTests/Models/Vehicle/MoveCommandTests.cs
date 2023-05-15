using AutoFixture;
using Moq;
using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Vehicle;
using Pss.ExampleRover.Domain.Services.Navigation;

namespace Pss.ExampleRover.Domain.UnitTests.Models.Vehicle
{
    [TestFixture]
    public class MoveCommandTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Ctor_MoveRoverServiceNull_ThrowsException()
        {
            Assert.That(() => new MoveCommand(_fixture.Create<MoveDirection>(), null),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("moveRoverService"));
        }

        [Test]
        public void Invoke_WhenCalled_UsesMoveService()
        {
            var moveServiceMock = new Mock<IMoveRoverService>();
            var direction = _fixture.Create<MoveDirection>();
            var deployedRover = _fixture.Create<DeployedRover>();

            var sut = new MoveCommand(direction, moveServiceMock.Object);
            sut.Invoke(deployedRover);

            moveServiceMock.Verify(m => m.Move(deployedRover, direction));
        }
    }
}