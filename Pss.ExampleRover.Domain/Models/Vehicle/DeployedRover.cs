using System;
using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Terrain;

namespace Pss.ExampleRover.Domain.Models.Vehicle
{
    public class DeployedRover
    {
        public DeployedRover(Planet planet, Coordinate initialCoordinate, Heading initialHeading)
        {
            Planet = planet;

            SetHeading(initialHeading);
            SetCoordinate(initialCoordinate);
        }

        public Planet Planet { get; }
        public Coordinate CurrentCoordinate { get; private set; }
        public Heading CurrentHeading { get; private set; }

        public NavigationResult Navigate(NavigationCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            return command.Invoke(this);
        }

        internal void SetHeading(Heading heading)
        {
            CurrentHeading = heading;
        }

        internal void SetCoordinate(Coordinate coordinate)
        {
            CurrentCoordinate = coordinate;
        }
    }
}