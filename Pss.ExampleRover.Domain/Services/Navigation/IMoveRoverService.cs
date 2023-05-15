using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Domain.Services.Navigation
{
    internal interface IMoveRoverService
    {
        NavigationResult Move(DeployedRover rover, MoveDirection moveDirection);
    }
}