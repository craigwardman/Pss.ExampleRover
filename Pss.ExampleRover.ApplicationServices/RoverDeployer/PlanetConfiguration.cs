using System.Collections.Generic;
using Pss.ExampleRover.Domain.Models.Location;

namespace Pss.ExampleRover.ApplicationServices.RoverDeployer
{
    public class PlanetConfiguration
    {
        public string Name { get; set; } = "Pluto";
        public uint GridWidth { get; set; }
        public uint GridHeight { get; set; }

        public List<Coordinate> Obstacles { get; } = new List<Coordinate>();
    }
}