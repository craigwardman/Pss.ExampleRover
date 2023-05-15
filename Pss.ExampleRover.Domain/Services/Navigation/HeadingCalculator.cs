using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Domain.Services.Navigation
{
    internal class HeadingCalculator : IHeadingCalculator
    {
        public Heading GetAdjustedHeading(Heading currentHeadingOrdinal, TurnDirection directionOffset)
        {
            return (Heading)(((int)currentHeadingOrdinal + (int)directionOffset) % 4);
        }
    }
}