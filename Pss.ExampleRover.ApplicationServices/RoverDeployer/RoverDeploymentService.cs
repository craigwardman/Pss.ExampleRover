using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Terrain;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.ApplicationServices.RoverDeployer
{
    public class RoverDeploymentService : IRoverDeploymentService
    {
        public DeployedRover DeployRover(IRoverDeploymentConfiguration configuration)
        {
            return new DeployedRover(
                new Planet(configuration.Planet.Name,
                    new SphericalGrid(configuration.Planet.GridWidth,
                        configuration.Planet.GridHeight),
                    configuration.Planet.Obstacles),
                new Coordinate(configuration.Rover.InitialX,
                    configuration.Rover.InitialY),
                configuration.Rover.InitialHeading);
        }
    }
}