using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Pss.ExampleRover.Api.Controllers;
using Pss.ExampleRover.ApplicationServices.RoverCommand;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Api.UnitTests.Controllers
{
    [TestFixture]
    public class RoverControllerTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IStringCommandProcessor> _mockStringCommandProcessor;
        private DeployedRover _stubRover;

        [SetUp]
        public void SetUp()
        {
            _mockStringCommandProcessor = new Mock<IStringCommandProcessor>();
            _stubRover = _fixture.Create<DeployedRover>();
        }

        [Test]
        public void Ctor_RoverNull_ThrowsException()
        {
            Assert.That(() => new RoverController(null, _mockStringCommandProcessor.Object),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("rover"));
        }

        [Test]
        public void Ctor_StringCommandProcessorNull_ThrowsException()
        {
            Assert.That(() => new RoverController(_fixture.Create<DeployedRover>(), null),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("stringCommandProcessor"));
        }

        [Test]
        public void MoveRover_WhenCalled_CallsProcessor()
        {
            var commandSequence = _fixture.Create<string>();
            
            var sut = GetDefaultSut();
            sut.MoveRover(commandSequence);

            _mockStringCommandProcessor.Verify(p => p.ProcessNavigationCommandSequence(_stubRover, commandSequence), Times.Once);
        }

        [Test]
        public void MoveRover_WhenProcessingComplete_ReturnsServiceResult()
        {
            var expected = _fixture.Create<CommandSequenceResult>();
            _mockStringCommandProcessor.Setup(p =>
                    p.ProcessNavigationCommandSequence(It.IsAny<DeployedRover>(), It.IsAny<string>()))
                .Returns(expected);

            var sut = GetDefaultSut();
            var result = sut.MoveRover(_fixture.Create<string>());

            result.Value.Should().Be(expected);
        }

        private RoverController GetDefaultSut() => new RoverController(_stubRover, _mockStringCommandProcessor.Object);
    }
}