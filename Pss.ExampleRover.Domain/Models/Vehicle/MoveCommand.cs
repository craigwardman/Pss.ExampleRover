using System;
using Pss.ExampleRover.Domain.Services.Navigation;

namespace Pss.ExampleRover.Domain.Models.Vehicle
{
    public class MoveCommand : NavigationCommand
    {
        private readonly IMoveRoverService _moveRoverService;

        public MoveCommand(MoveDirection moveDirection)
        : this(moveDirection, new MoveRoverService())
        {
        }

        internal MoveCommand(MoveDirection moveDirection, IMoveRoverService moveRoverService)
        {
            Direction = moveDirection;
            _moveRoverService = moveRoverService ?? throw new ArgumentNullException(nameof(moveRoverService));
        }

        public MoveDirection Direction { get; }

        protected internal override NavigationResult Invoke(DeployedRover rover)
        {
            return _moveRoverService.Move(rover, Direction);
        }
    }
}