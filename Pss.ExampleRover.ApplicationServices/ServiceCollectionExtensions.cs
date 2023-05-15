using Microsoft.Extensions.DependencyInjection;
using Pss.ExampleRover.ApplicationServices.RoverCommand;
using Pss.ExampleRover.ApplicationServices.RoverDeployer;

namespace Pss.ExampleRover.ApplicationServices
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IStringCommandProcessor, StringCommandProcessor>();
            services.AddTransient<ICommandFactory, CommandFactory>();
            services.AddTransient<IRoverDeploymentService, RoverDeploymentService>();

            // deploy a single rover per api instance:
            services.AddSingleton(provider =>
            {
                var deployer = provider.GetRequiredService<IRoverDeploymentService>();
                var config = provider.GetRequiredService<IRoverDeploymentConfiguration>();

                return deployer.DeployRover(config);
            });
        }
    }
}