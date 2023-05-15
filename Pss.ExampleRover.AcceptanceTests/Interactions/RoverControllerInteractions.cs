using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Pss.ExampleRover.AcceptanceTests.Context;

namespace Pss.ExampleRover.AcceptanceTests.Interactions
{
    public class RoverControllerInteractions
    {
        private readonly HttpClient _client;

        public RoverControllerInteractions(WebTestFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        public async Task<CommandSequenceResult> MoveRover(string commandSequence)
        {
            var response = await _client.PostAsJsonAsync("api/Rover", commandSequence);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Assert.Fail(responseContent);
            }

            return await response.Content.ReadAsAsync<CommandSequenceResult>();
        }
    }
}