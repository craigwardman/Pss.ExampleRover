using Pss.ExampleRover.Domain.Models.Location;

namespace Pss.ExampleRover.ApplicationServices.RoverCommand
{
    public class CommandSequenceResult
    {
        public CommandSequenceResult(Coordinate finalCoordinate, Heading finalHeading, bool obstacleEncountered)
        {
            FinalCoordinate = finalCoordinate;
            FinalHeading = finalHeading;
            ObstacleEncountered = obstacleEncountered;
        }

        public Coordinate FinalCoordinate { get; }
        public Heading FinalHeading { get; }
        public bool ObstacleEncountered { get; }
    }
}