using AutoFixture;
using NUnit.Framework;
using Pss.ExampleRover.Domain.Models.Location;

namespace Pss.ExampleRover.Domain.UnitTests.Models.Location
{
    [TestFixture]
    public class CoordinateTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Ctor_WhenCalled_CorrectlyInitializes()
        {
            var x = _fixture.Create<int>();
            var y = _fixture.Create<int>();

            var sut = new Coordinate(x, y);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(x, sut.X);
                Assert.AreEqual(y, sut.Y);
            });
        }
    }
}