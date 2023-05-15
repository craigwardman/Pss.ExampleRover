using System;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Domain.Services.Navigation
{
    internal class TurnRoverService : ITurnRoverService
    {
        private readonly IHeadingCalculator _headingCalculator;

        public TurnRoverService()
        : this(new HeadingCalculator())
        {
        }

        internal TurnRoverService(IHeadingCalculator headingCalculator)
        {
            _headingCalculator = headingCalculator ?? throw new ArgumentNullException(nameof(headingCalculator));
        }

        public NavigationResult Turn(DeployedRover rover, TurnDirection turnDirection)
        {
            if (rover == null) throw new ArgumentNullException(nameof(rover));

            var newHeading = _headingCalculator.GetAdjustedHeading(rover.CurrentHeading, turnDirection);
            rover.SetHeading(newHeading);

            return NavigationResult.Success;
        }
    }
}