using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.ApplicationServices.RoverDeployer
{
    public interface IRoverDeploymentService
    {
        DeployedRover DeployRover(IRoverDeploymentConfiguration configuration);
    }
}