using System;
using System.Linq;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.ApplicationServices.RoverCommand
{
    public class StringCommandProcessor : IStringCommandProcessor
    {
        private readonly ICommandFactory _commandFactory;

        public StringCommandProcessor(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
        }

        public CommandSequenceResult ProcessNavigationCommandSequence(DeployedRover rover, string sequence)
        {
            if (string.IsNullOrEmpty(sequence))
                return new CommandSequenceResult(rover.CurrentCoordinate, rover.CurrentHeading, false);

            if (rover == null) throw new ArgumentNullException(nameof(rover));

            var obstacleEncountered = sequence.Select(t => _commandFactory.Create(t))
                .Where(command => command != null)
                .Select(rover.Navigate)
                .Any(result => result == NavigationResult.FailedDueToObstacle);

            return new CommandSequenceResult(
                rover.CurrentCoordinate, rover.CurrentHeading, obstacleEncountered);
        }
    }
}