using CustomerJourney.API.Services;
using CustomerJourney_API.Models.Request;
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
        public IResult GetPersonas([FromBody] MarketingRequest request)
        {
            _response.FunctionName = "Personas";

            this._logger.LogDebug("Personas receive request.");

            
            if (string.IsNullOrEmpty(request.Brand) || string.IsNullOrEmpty(request.Language) || string.IsNullOrEmpty(request.Industry))
            {
                return TypedResults.BadRequest("Brand, Language and Industry are required.");
            }

            Task.Run(() => _response.GetAsync("Personas",
                new Dictionary<string, string>()
                {
                    { "input", request.Brand },
                    { "language", request.Language },
                    { "count", request.Count.ToString() },
                    { "industry", request.Industry }
                }));

            return TypedResults.Ok("Persona requested");
        }

    }
}
