using System.Threading.Tasks;
using NUnit.Framework;
using Pss.ExampleRover.AcceptanceTests.Context;
using Pss.ExampleRover.AcceptanceTests.Interactions;
using Pss.ExampleRover.Domain.Models.Location;
using TechTalk.SpecFlow;

namespace Pss.ExampleRover.AcceptanceTests.Steps
{
    [Binding]
    public class RoverControlsSteps
    {
        private readonly DeploymentConfiguration _configurationContext;
        private readonly RoverControllerInteractions _roverControllerInteractions;
        private readonly RoverControllerContext _roverControllerContext;

        public RoverControlsSteps(DeploymentConfiguration configurationContext,
            RoverControllerInteractions roverControllerInteractions, RoverControllerContext roverControllerContext)
        {
            _configurationContext = configurationContext;
            _roverControllerInteractions = roverControllerInteractions;
            _roverControllerContext = roverControllerContext;
        }

        [Given(@"the rover is located at (.*),(.*)")]
        public void GivenTheRoverIsLocatedAt(int x, int y)
        {
            _configurationContext.Rover.InitialX = x;
            _configurationContext.Rover.InitialY = y;
        }

        [Given(@"the rover is facing (.*)")]
        public void GivenTheRoverIsFacingNorth(Heading heading)
        {
            _configurationContext.Rover.InitialHeading = heading;
        }

        [Given(@"the grid is (.*)x(.*)")]
        public void GivenTheGridIs(uint width, uint height)
        {
            _configurationContext.Planet.GridWidth = width;
            _configurationContext.Planet.GridHeight = height;
        }

        [Given(@"there is an obstacle at (.*),(.*)")]
        public void GivenThereIsAnObstacleAt(int x, int y)
        {
            _configurationContext.Planet.Obstacles.Add(new Coordinate(x, y));
        }

        [When(@"the command sequence is ""(.*)""")]
        public async Task WhenTheCommandSequenceIs(string commands)
        {
            _roverControllerContext.Result = await _roverControllerInteractions.MoveRover(commands);
        }

        [Then(@"the rover should be located at (.*),(.*)")]
        public void ThenTheRoverShouldBeLocatedAt(int x, int y)
        {
            Assert.Multiple(() =>
                {
                    Assert.AreEqual(x, _roverControllerContext.Result?.FinalCoordinate.X);
                    Assert.AreEqual(y, _roverControllerContext.Result?.FinalCoordinate.Y);
                });
        }

        [Then(@"the rover should be facing (.*)")]
        public void ThenTheRoverShouldBeFacingEast(Heading heading)
        {
            Assert.AreEqual(heading, _roverControllerContext.Result?.FinalHeading);
        }

        [Then(@"the rover returned an obstacle blockage message")]
        public void ThenTheRoverReturnedAnObstacleBlockageMessage()
        {
            Assert.IsTrue(_roverControllerContext.Result?.ObstacleEncountered);
        }
    }
}