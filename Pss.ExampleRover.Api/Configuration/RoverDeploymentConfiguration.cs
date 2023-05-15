using System;
using Microsoft.Extensions.Configuration;
using Pss.ExampleRover.ApplicationServices.RoverDeployer;

namespace Pss.ExampleRover.Api.Configuration
{
    public class RoverDeploymentConfiguration : IRoverDeploymentConfiguration
    {
        public RoverDeploymentConfiguration(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            configuration.Bind("RoverDeploymentConfiguration", this);
        }

        public PlanetConfiguration Planet { get; set; }
        public RoverConfiguration Rover { get; set; }
    }
}