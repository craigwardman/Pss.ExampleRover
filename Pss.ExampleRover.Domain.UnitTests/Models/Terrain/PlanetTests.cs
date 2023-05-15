using System.Collections.Generic;
using AutoFixture;
using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Terrain;

namespace Pss.ExampleRover.Domain.UnitTests.Models.Terrain
{
    [TestFixture]
    public class PlanetTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Ctor_GridIsNull_ThrowsException()
        {
            Assert.That(() => new Planet(_fixture.Create<string>(), null, _fixture.Create<IReadOnlyList<Coordinate>>()), 
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("grid"));
        }

        [Test]
        public void Ctor_ObstaclesIsNull_ThrowsException()
        {
            Assert.That(() => new Planet(_fixture.Create<string>(), _fixture.Create<SphericalGrid>(), null),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("obstacles"));
        }

        [Test]
        public void Ctor_WhenCalled_CorrectlyInitializes()
        {
            var name = _fixture.Create<string>();

            var sut = new Planet(name, _fixture.Create<SphericalGrid>(), _fixture.Create<IReadOnlyList<Coordinate>>());

            Assert.AreEqual(name, sut.Name);
        }

        [Test]
        public void GetGridCoordinate_WhenCalled_UsesSphericalGridCoordinate()
        {
            var sphericalGrid = _fixture.Create<SphericalGrid>();
            var coordinate = _fixture.Create<Coordinate>();
            var expected = sphericalGrid.GetGridCoordinate(coordinate);

            var sut = new Planet(_fixture.Create<string>(), sphericalGrid, _fixture.Create<IReadOnlyList<Coordinate>>());
            var result = sut.GetGridCoordinate(coordinate);

            Assert.AreEqual(expected, result);
        }

        [TestCase(0, 0, false)]
        [TestCase(0, 1, true)]
        [TestCase(1, 0, true)]
        [TestCase(1, 1, false)]
        public void HasObstacle_WhenCalled_ReturnsExpected(int x, int y, bool expected)
        {
            var obstacles = new[]
            {
                new Coordinate(1, 0),
                new Coordinate(0, 1)
            };
            
            var sut = new Planet(_fixture.Create<string>(), new SphericalGrid(2, 2), obstacles);
            var result = sut.HasObstacle(new Coordinate(x, y));

            Assert.AreEqual(result, expected);
        }
    }
}