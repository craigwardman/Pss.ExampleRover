using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.ApplicationServices.RoverCommand
{
    public interface ICommandFactory
    {
        NavigationCommand Create(char command);
    }
}