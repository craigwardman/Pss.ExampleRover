using Pss.ExampleRover.Domain.Models.Location;

namespace Pss.ExampleRover.AcceptanceTests.Context
{
    public class CommandSequenceResult
    {
        public Coordinate FinalCoordinate { get; set; }
        public Heading FinalHeading { get; set; }
        public bool ObstacleEncountered { get; set; }
    }
}