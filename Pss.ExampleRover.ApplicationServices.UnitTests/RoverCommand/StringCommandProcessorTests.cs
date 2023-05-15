using System;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Pss.ExampleRover.ApplicationServices.RoverCommand;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.ApplicationServices.UnitTests.RoverCommand
{
    [TestFixture]
    public class StringCommandProcessorTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<ICommandFactory> _commandFactoryMock;

        [SetUp]
        public void SetUp()
        {
            _commandFactoryMock = new Mock<ICommandFactory>();
        }

        [Test]
        public void Ctor_CommandFactoryNull_ThrowsException()
        {
            Func<StringCommandProcessor> act = () => new StringCommandProcessor(null);

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("commandFactory");
        }

        [Test]
        public void ProcessNavigationCommandSequence_RoverNull_ThrowsException()
        {
            var sut = GetDefaultSut();
            sut.Invoking(s => s.ProcessNavigationCommandSequence(null, _fixture.Create<string>()))
                .Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("rover");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ProcessNavigationCommandSequence_CommandNullOrEmpty_ReturnsCurrentRoverData(string command)
        {
            var rover = _fixture.Create<DeployedRover>();
            var expected = new CommandSequenceResult(rover.CurrentCoordinate, rover.CurrentHeading, false);

            var sut = GetDefaultSut();
            var result = sut.ProcessNavigationCommandSequence(rover, command);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ProcessNavigationCommandSequence_WhenCalled_IteratesCommandSequence()
        {
            var commandSequence = _fixture.Create<string>();
            var fakeCommand = new Mock<NavigationCommand>();
            _commandFactoryMock.Setup(f => f.Create(It.IsAny<char>())).Returns(fakeCommand.Object);
            var deployedRover = _fixture.Create<DeployedRover>();

            var sut = GetDefaultSut();
            sut.ProcessNavigationCommandSequence(deployedRover, commandSequence);

            using (new AssertionScope())
            {
                _commandFactoryMock.Verify(f => f.Create(It.IsAny<char>()), Times.Exactly(commandSequence.Length));
                fakeCommand.Protected().Verify("Invoke", Times.Exactly(commandSequence.Length), deployedRover);
            }
        }

        [Test]
        public void ProcessNavigationCommandSequence_WhenCalledWithObstacleStoppage_OnlyIteratesCommandSequenceUntilStoppage()
        {
            var commandSequence = _fixture.Create<string>();
            var deployedRover = _fixture.Create<DeployedRover>();
            var fakeCommand = new Mock<NavigationCommand>();
            fakeCommand.Protected().Setup<NavigationResult>("Invoke", deployedRover)
                .Returns(NavigationResult.FailedDueToObstacle);
            _commandFactoryMock.Setup(f => f.Create(It.IsAny<char>())).Returns(fakeCommand.Object);

            var sut = GetDefaultSut();
            sut.ProcessNavigationCommandSequence(deployedRover, commandSequence);

            using (new AssertionScope())
            {
                _commandFactoryMock.Verify(f => f.Create(It.IsAny<char>()), Times.Once);
                fakeCommand.Protected().Verify("Invoke", Times.Once(), deployedRover);
            }
        }

        private StringCommandProcessor GetDefaultSut() => new StringCommandProcessor(_commandFactoryMock.Object);
    }
}