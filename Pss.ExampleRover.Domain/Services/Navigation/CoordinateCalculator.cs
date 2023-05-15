using System.Collections.Generic;
using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Domain.Services.Navigation
{
    internal class CoordinateCalculator : ICoordinateCalculator
    {
        private static readonly HashSet<Heading> NegatedControlHeadings = new HashSet<Heading>(
            new [] {Heading.South, Heading.West});
        private static readonly HashSet<Heading> XAxisHeadings = new HashSet<Heading>(
            new[] { Heading.East, Heading.West });
        private static readonly HashSet<Heading> YAxisHeadings = new HashSet<Heading>(
            new[] { Heading.North, Heading.South });

        public Coordinate CalculateNextCoordinate(Coordinate currentCoordinate, Heading currentHeading,
            MoveDirection moveDirection)
        {
            var x = XAxisHeadings.Contains(currentHeading) ? GetAdjustedAxis(currentCoordinate.X, currentHeading, moveDirection) : currentCoordinate.X;
            var y = YAxisHeadings.Contains(currentHeading) ? GetAdjustedAxis(currentCoordinate.Y, currentHeading, moveDirection) : currentCoordinate.Y;

            return new Coordinate(x, y);
        }

        private static int GetAdjustedAxis(int currentPoint, Heading heading, MoveDirection moveDirection)
        {
            var negateDirection = NegatedControlHeadings.Contains(heading);
            var directionAdjustment = negateDirection ? (int)moveDirection * -1 : (int)moveDirection;

            return currentPoint + directionAdjustment;
        }
    }
}