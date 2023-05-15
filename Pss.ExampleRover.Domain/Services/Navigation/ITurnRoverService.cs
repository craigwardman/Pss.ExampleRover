using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Domain.Services.Navigation
{
    internal interface ITurnRoverService
    {
        NavigationResult Turn(DeployedRover rover, TurnDirection turnDirection);
    }
}