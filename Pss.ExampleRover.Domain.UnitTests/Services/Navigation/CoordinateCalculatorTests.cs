using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Vehicle;
using Pss.ExampleRover.Domain.Services.Navigation;

namespace Pss.ExampleRover.Domain.UnitTests.Services.Navigation
{
    [TestFixture]
    public class CoordinateCalculatorTests
    {
        [TestCase(Heading.North, MoveDirection.Forward, 0, 1)]
        [TestCase(Heading.North, MoveDirection.Backward, 0, -1)]
        [TestCase(Heading.East, MoveDirection.Forward, 1, 0)]
        [TestCase(Heading.East, MoveDirection.Backward, -1, 0)]
        [TestCase(Heading.South, MoveDirection.Forward, 0, -1)]
        [TestCase(Heading.South, MoveDirection.Backward, 0, 1)]
        [TestCase(Heading.West, MoveDirection.Forward, -1, 0)]
        [TestCase(Heading.West, MoveDirection.Backward, 1, 0)]
        public void CalculateNextCoordinate_ForGivenDirectionAndHeading_ReturnsExpected(Heading heading, MoveDirection direction, int expectedX, int expectedY)
        {
            var currentCoordinate = new Coordinate(0, 0);

            var sut = new CoordinateCalculator();
            var result = sut.CalculateNextCoordinate(currentCoordinate, heading, direction);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedX, result.X);
                Assert.AreEqual(expectedY, result.Y);
            });
        }
    }
}