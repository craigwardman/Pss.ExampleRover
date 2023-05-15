using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Pss.ExampleRover.AcceptanceTests.Context;
using Pss.ExampleRover.Api;
using Pss.ExampleRover.ApplicationServices.RoverDeployer;

namespace Pss.ExampleRover.AcceptanceTests
{
    public class WebTestFixture : WebApplicationFactory<Startup>
    {
        private readonly DeploymentConfiguration _deploymentConfigurationContext;

        public WebTestFixture(DeploymentConfiguration deploymentConfigurationContext)
        {
            _deploymentConfigurationContext = deploymentConfigurationContext;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IRoverDeploymentConfiguration>(_deploymentConfigurationContext);
            });
        }
    }
}