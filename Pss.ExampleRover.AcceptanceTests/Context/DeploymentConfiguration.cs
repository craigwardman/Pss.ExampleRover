using Pss.ExampleRover.ApplicationServices.RoverDeployer;
using Pss.ExampleRover.Domain.Models.Location;

namespace Pss.ExampleRover.AcceptanceTests.Context
{
    public class DeploymentConfiguration : IRoverDeploymentConfiguration
    {
        public PlanetConfiguration Planet { get; } = new PlanetConfiguration
        {
            GridHeight = 100,
            GridWidth = 100,
            Name = "Pluto"
        };

        public RoverConfiguration Rover { get; } = new RoverConfiguration
        {
            InitialHeading = Heading.North,
            InitialX = 0,
            InitialY = 0
        };
    }
}