using System;
using System.Collections.Generic;
using Pss.ExampleRover.Domain.Models.Location;

namespace Pss.ExampleRover.Domain.Models.Terrain
{
    public class Planet
    {
        private readonly SphericalGrid _grid;
        private readonly HashSet<Coordinate> _obstacleMap;

        public Planet(string name, SphericalGrid grid, IReadOnlyList<Coordinate> obstacles)
        {
            if (obstacles == null) throw new ArgumentNullException(nameof(obstacles));

            Name = name;
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _obstacleMap = new HashSet<Coordinate>(obstacles);
        }

        public string Name { get; }

        public Coordinate GetGridCoordinate(Coordinate unboundedCoordinate)
        {
            return _grid.GetGridCoordinate(unboundedCoordinate);
        }

        public bool HasObstacle(Coordinate coordinate)
        {
            return _obstacleMap.Contains(coordinate);
        }
    }
}