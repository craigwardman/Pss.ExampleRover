using System;
using Pss.ExampleRover.Domain.Models.Location;

namespace Pss.ExampleRover.Domain.Models.Terrain
{
    public class SphericalGrid
    {
        private readonly uint _width;
        private readonly uint _height;

        public SphericalGrid(uint width, uint height)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

            _width = width;
            _height = height;
        }

        public Coordinate GetGridCoordinate(Coordinate unboundedCoordinate)
        {
            return new Coordinate(
                (int) ((_width + unboundedCoordinate.X) % _width),
                (int) ((_height + unboundedCoordinate.Y) % _height));
        }
    }
}