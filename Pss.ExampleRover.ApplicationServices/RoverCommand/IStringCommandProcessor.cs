using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.ApplicationServices.RoverCommand
{
    public interface IStringCommandProcessor
    {
        CommandSequenceResult ProcessNavigationCommandSequence(DeployedRover rover, string sequence);
    }
}