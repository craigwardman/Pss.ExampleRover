namespace Pss.ExampleRover.Domain.Models.Vehicle
{
    public abstract class NavigationCommand
    {
        protected internal abstract NavigationResult Invoke(DeployedRover rover);
    }
}