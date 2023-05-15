using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Vehicle;
using Pss.ExampleRover.Domain.Services.Navigation;

namespace Pss.ExampleRover.Domain.UnitTests.Services.Navigation
{
    [TestFixture]
    public class HeadingCalculatorTests
    {
        [TestCase(Heading.North, TurnDirection.Right, Heading.East)]
        [TestCase(Heading.North, TurnDirection.Left, Heading.West)]
        [TestCase(Heading.East, TurnDirection.Right, Heading.South)]
        [TestCase(Heading.East, TurnDirection.Left, Heading.North)]
        [TestCase(Heading.South, TurnDirection.Right, Heading.West)]
        [TestCase(Heading.South, TurnDirection.Left, Heading.East)]
        [TestCase(Heading.West, TurnDirection.Right, Heading.North)]
        [TestCase(Heading.West, TurnDirection.Left, Heading.South)]
        public void GetAdjustedHeading_ForGivenHeadingAndTurnDirection_ReturnsExpected(Heading currentHeading, TurnDirection direction, Heading expected)
        {
            var sut = new HeadingCalculator();
            var result = sut.GetAdjustedHeading(currentHeading, direction);

            Assert.AreEqual(expected, result);
        }
    }
}