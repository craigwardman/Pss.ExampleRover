using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Domain.Services.Navigation
{
    internal interface ICoordinateCalculator
    {
        Coordinate CalculateNextCoordinate(Coordinate currentCoordinate, Heading currentHeading,
            MoveDirection moveDirection);
    }
}