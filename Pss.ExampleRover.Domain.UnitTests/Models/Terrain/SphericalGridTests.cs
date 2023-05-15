using System;
using AutoFixture;
using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Location;
using Pss.ExampleRover.Domain.Models.Terrain;

namespace Pss.ExampleRover.Domain.UnitTests.Models.Terrain
{
    [TestFixture]
    public class SphericalGridTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Ctor_WidthOutOfRange_ThrowsException()
        {
            Assert.That(() => new SphericalGrid(0, _fixture.Create<uint>()),
                Throws.Exception.TypeOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("width"));
        }

        [Test]
        public void Ctor_HeightOutOfRange_ThrowsException()
        {
            Assert.That(() => new SphericalGrid(_fixture.Create<uint>(), 0),
                Throws.Exception.TypeOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("height"));
        }

        [Test]
        public void GetGridCoordinate_WhenWithinBounds_ReturnsUnbounded()
        {
            var gridWidth = _fixture.Create<uint>();
            var gridHeight = _fixture.Create<uint>();
            var x = (int)gridWidth - 1;
            var y = (int)gridHeight - 1;
            
            var sut = new SphericalGrid(gridWidth, gridHeight);
            var result = sut.GetGridCoordinate(new Coordinate(x, y));

            Assert.Multiple(() =>
            {
                Assert.AreEqual(x, result.X);
                Assert.AreEqual(y, result.Y);
            });
        }

        [Test]
        public void GetGridCoordinate_WhenPositiveOutOfBounds_ReturnsWrappedCoordinate()
        {
            var gridWidth = _fixture.Create<uint>();
            var gridHeight = _fixture.Create<uint>();
            var x = (int)gridWidth + 1;
            var y = (int)gridHeight + 1;

            var sut = new SphericalGrid(gridWidth, gridHeight);
            var result = sut.GetGridCoordinate(new Coordinate(x, y));

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, result.X);
                Assert.AreEqual(1, result.Y);
            });
        }

        [Test]
        public void GetGridCoordinate_WhenNegativeOutOfBounds_ReturnsWrappedCoordinate()
        {
            var gridWidth = _fixture.Create<uint>();
            var gridHeight = _fixture.Create<uint>();

            var sut = new SphericalGrid(gridWidth, gridHeight);
            var result = sut.GetGridCoordinate(new Coordinate(-1, -1));

            Assert.Multiple(() =>
            {
                Assert.AreEqual(gridWidth -1, result.X);
                Assert.AreEqual(gridHeight - 1, result.Y);
            });
        }
    }
}