using Pss.ExampleRover.Domain.Models.Location;

namespace Pss.ExampleRover.ApplicationServices.RoverDeployer
{
    public class RoverConfiguration
    {
        public int InitialX { get; set; }
        public int InitialY { get; set; }
        public Heading InitialHeading { get; set; }
    }
}