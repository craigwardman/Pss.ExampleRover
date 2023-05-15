using System.Linq;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Pss.ExampleRover.ApplicationServices.RoverCommand;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.ApplicationServices.UnitTests.RoverCommand
{
    [TestFixture]
    public class CommandFactoryTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestCase('F', MoveDirection.Forward)]
        [TestCase('B', MoveDirection.Backward)]
        public void Create_WhenKnownMoveCommand_ReturnsExpected(char command, MoveDirection direction)
        {
            var sut = new CommandFactory();
            var result = sut.Create(command);

            result.Should().BeOfType<MoveCommand>().Which.Direction.Should().Be(direction);
        }

        [TestCase('L', TurnDirection.Left)]
        [TestCase('R', TurnDirection.Right)]
        public void Create_WhenKnownTurnCommand_ReturnsExpected(char command, TurnDirection direction)
        {
            var sut = new CommandFactory();
            var result = sut.Create(command);

            result.Should().BeOfType<TurnCommand>().Which.Direction.Should().Be(direction);
        }

        [Test]
        public void Create_WhenUnknownCommand_ReturnsNull()
        {
            var command = _fixture.Create<Generator<char>>().First(c => !"FBLR".Contains(c));
            
            var sut = new CommandFactory();
            var result = sut.Create(command);

            result.Should().BeNull();
        }
    }
}