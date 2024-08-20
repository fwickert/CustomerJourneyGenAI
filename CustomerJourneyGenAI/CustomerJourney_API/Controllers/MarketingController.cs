using CustomerJourney.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerJourney_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingController : ControllerBase
    {

        private readonly ILogger<MarketingController> _logger;

        private readonly LLMResponse _response;

        public MarketingController(ILogger<MarketingController> logger, LLMResponse response)
        {
            _logger = logger;
            _response = response;
            _response.PluginName = "MarketingPlugin";

        }

        [HttpPost("personas", Name = "personas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetPersonas(string brand, string language, int count, string industry)
        {
            _response.FunctionName = "Personas";

            this._logger.LogDebug("Personas receive request.");

            Task.Run(() => _response.GetAsync("Personas",
                new Dictionary<string, string>()
                {
                    { "input", brand },
                    { "language", language },
                    { "count", count.ToString() },
                    { "industry", industry }
                }));

            return TypedResults.Ok("Persona requested");
        }

    }
}
