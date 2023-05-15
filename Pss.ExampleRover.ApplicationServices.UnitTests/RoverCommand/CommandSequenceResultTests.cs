using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Pss.ExampleRover.ApplicationServices.RoverCommand;
using Pss.ExampleRover.Domain.Models.Location;

namespace Pss.ExampleRover.ApplicationServices.UnitTests.RoverCommand
{
    [TestFixture]
    public class CommandSequenceResultTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Ctor_WhenCalled_CorrectlyInitializes()
        {
            var expected = new
            {
                FinalCoordinate = _fixture.Create<Coordinate>(),
                FinalHeading = _fixture.Create<Heading>(),
                ObstacleEncountered = _fixture.Create<bool>()
            };

            var sut = new CommandSequenceResult(expected.FinalCoordinate, expected.FinalHeading, expected.ObstacleEncountered);

            sut.Should().BeEquivalentTo(expected);
        }
    }
}