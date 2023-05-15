using System;
using Pss.ExampleRover.Domain.Services.Navigation;

namespace Pss.ExampleRover.Domain.Models.Vehicle
{
    public class TurnCommand : NavigationCommand
    {
        private readonly ITurnRoverService _turnRoverService;

        public TurnCommand(TurnDirection turnDirection)
        : this(turnDirection, new TurnRoverService())
        {
        }

        internal TurnCommand(TurnDirection turnDirection, ITurnRoverService turnRoverService)
        {
            Direction = turnDirection;
            _turnRoverService = turnRoverService ?? throw new ArgumentNullException(nameof(turnRoverService));
        }

        public TurnDirection Direction { get; }

        protected internal override NavigationResult Invoke(DeployedRover rover)
        {
            return _turnRoverService.Turn(rover, Direction);
        }
    }
}