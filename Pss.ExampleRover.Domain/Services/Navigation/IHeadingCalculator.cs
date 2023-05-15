using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Domain.Services.Navigation
{
    internal interface IHeadingCalculator
    {
        Heading GetAdjustedHeading(Heading currentHeadingOrdinal, TurnDirection directionOffset);
    }
}