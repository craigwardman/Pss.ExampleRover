using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Pss.ExampleRover.Api.Configuration;
using Pss.ExampleRover.ApplicationServices.RoverDeployer;

namespace Pss.ExampleRover.Api.UnitTests.Configuration
{
    [TestFixture]
    public class RoverDeploymentConfigurationTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Dictionary<string, string> _stubConfigValues;

        [SetUp]
        public void SetUp()
        {
            _stubConfigValues = new Dictionary<string, string>();
        }

        [Test]
        public void Ctor_ConfigurationIsNull_ThrowsException()
        {
            Assert.That(() => new RoverDeploymentConfiguration(null),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("configuration"));
        }

        [Test]
        public void Ctor_WithConfiguration_BindsPlanet()
        {
            var stubPlanetConfiguration = _fixture.Create<PlanetConfiguration>();
            _stubConfigValues.Add("RoverDeploymentConfiguration:Planet:Name", stubPlanetConfiguration.Name);
            _stubConfigValues.Add("RoverDeploymentConfiguration:Planet:GridHeight", stubPlanetConfiguration.GridHeight.ToString());
            _stubConfigValues.Add("RoverDeploymentConfiguration:Planet:GridWidth", stubPlanetConfiguration.GridWidth.ToString());
            for (var i = 0; i < stubPlanetConfiguration.Obstacles.Count; i++)
            {
                _stubConfigValues.Add($"RoverDeploymentConfiguration:Planet:Obstacles:{i}:X", stubPlanetConfiguration.Obstacles[i].X.ToString());
                _stubConfigValues.Add($"RoverDeploymentConfiguration:Planet:Obstacles:{i}:Y", stubPlanetConfiguration.Obstacles[i].Y.ToString());
            }

            var sut = GetDefaultSut();

            sut.Planet.Should().BeEquivalentTo(stubPlanetConfiguration);
        }

        [Test]
        public void Ctor_WithConfiguration_BindsRover()
        {
            var stubRoverConfiguration = _fixture.Create<RoverConfiguration>();
            _stubConfigValues.Add("RoverDeploymentConfiguration:Rover:InitialX", stubRoverConfiguration.InitialX.ToString());
            _stubConfigValues.Add("RoverDeploymentConfiguration:Rover:InitialY", stubRoverConfiguration.InitialY.ToString());
            _stubConfigValues.Add("RoverDeploymentConfiguration:Rover:InitialHeading", stubRoverConfiguration.InitialHeading.ToString());

            var sut = GetDefaultSut();

            sut.Rover.Should().BeEquivalentTo(stubRoverConfiguration);
        }

        private RoverDeploymentConfiguration GetDefaultSut() => new RoverDeploymentConfiguration(new ConfigurationBuilder().AddInMemoryCollection(_stubConfigValues).Build());
    }
}