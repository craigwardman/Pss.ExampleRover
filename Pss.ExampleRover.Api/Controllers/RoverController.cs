using System;
using Microsoft.AspNetCore.Mvc;
using Pss.ExampleRover.ApplicationServices.RoverCommand;
using Pss.ExampleRover.Domain.Models.Vehicle;

namespace Pss.ExampleRover.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoverController : ControllerBase
    {
        private readonly DeployedRover _rover;
        private readonly IStringCommandProcessor _stringCommandProcessor;

        public RoverController(DeployedRover rover, IStringCommandProcessor stringCommandProcessor)
        {
            _rover = rover ?? throw new ArgumentNullException(nameof(rover));
            _stringCommandProcessor = stringCommandProcessor ?? throw new ArgumentNullException(nameof(stringCommandProcessor));
        }

        [HttpPost]
        public ActionResult<CommandSequenceResult> MoveRover([FromBody] string commandSequence)
        {
            var result = _stringCommandProcessor.ProcessNavigationCommandSequence(_rover, commandSequence);

            return result;
        }
    }
}