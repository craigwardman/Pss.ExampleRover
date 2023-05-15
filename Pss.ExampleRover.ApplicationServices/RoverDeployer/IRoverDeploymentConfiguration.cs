namespace Pss.ExampleRover.ApplicationServices.RoverDeployer
{
    public interface IRoverDeploymentConfiguration
    {
        PlanetConfiguration Planet { get; }
        RoverConfiguration Rover { get; }
    }
}