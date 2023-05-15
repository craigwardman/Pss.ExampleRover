using System;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Domain.Services.Navigation
{
    internal class MoveRoverService : IMoveRoverService
    {
        private readonly ICoordinateCalculator _coordinateCalculator;

        public MoveRoverService()
        : this(new CoordinateCalculator())
        {
        }

        internal MoveRoverService(ICoordinateCalculator coordinateCalculator)
        {
            _coordinateCalculator = coordinateCalculator ?? throw new ArgumentNullException(nameof(coordinateCalculator));
        }

        public NavigationResult Move(DeployedRover rover, MoveDirection moveDirection)
        {
            if (rover == null) throw new ArgumentNullException(nameof(rover));

            var nextCoordinate = _coordinateCalculator.CalculateNextCoordinate(rover.CurrentCoordinate, rover.CurrentHeading, moveDirection);
            var planetCoordinate = rover.Planet.GetGridCoordinate(nextCoordinate);

            if (rover.Planet.HasObstacle(planetCoordinate))
            {
                return NavigationResult.FailedDueToObstacle;
            }

            rover.SetCoordinate(planetCoordinate);
            return NavigationResult.Success;
        }
    }
}