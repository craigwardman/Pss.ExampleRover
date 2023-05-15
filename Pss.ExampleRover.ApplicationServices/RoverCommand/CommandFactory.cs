using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.ApplicationServices.RoverCommand
{
    internal class CommandFactory : ICommandFactory
    {
        public NavigationCommand Create(char command)
        {
            switch (command)
            {
                case 'F':
                    return new MoveCommand(MoveDirection.Forward);
                case 'B':
                    return new MoveCommand(MoveDirection.Backward);
                case 'L':
                    return new TurnCommand(TurnDirection.Left);
                case 'R':
                    return new TurnCommand(TurnDirection.Right);
                default:
                    return null;
            }
        }
    }
}