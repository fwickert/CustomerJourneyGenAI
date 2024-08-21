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
        public IResult GetPersonas([FromBody] MarketingRequest request, string connectionId)
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
                }, connectionId)) ;

            return TypedResults.Ok("Persona requested");
        }

        //Same for GetSegments
        [HttpPost("segments", Name = "segments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetSegments([FromBody] MarketingRequest request, string connectionId)
        {
            _response.FunctionName = "Segments";

            this._logger.LogDebug("Segments receive request.");

            if (string.IsNullOrEmpty(request.Brand) || string.IsNullOrEmpty(request.Language) || string.IsNullOrEmpty(request.Industry) || string.IsNullOrEmpty(request.Memory))
            {
                return TypedResults.BadRequest("Personas and other info are required.");
            }

            Task.Run(() => _response.GetAsync("Segments",
                new Dictionary<string, string>()
                {
                    { "brand", request.Brand },
                    { "language", request.Language },
                    { "count", request.Count.ToString() },
                    { "industry", request.Industry },
                    { "memory", request.Memory }
                }, connectionId));

            return TypedResults.Ok("Segments requested");
        }

        //same for GetRecommendations
        [HttpPost("recommendations", Name = "recommendations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetRecommendations([FromBody] MarketingRequest request, string connectionId)
        {
            _response.FunctionName = "Recommendations";

            this._logger.LogDebug("Recommendations receive request.");

            if (string.IsNullOrEmpty(request.Brand) || string.IsNullOrEmpty(request.Language) || string.IsNullOrEmpty(request.Industry) || string.IsNullOrEmpty(request.Memory))
            {
                return TypedResults.BadRequest("Personas and other info are required.");
            }

            Task.Run(() => _response.GetAsync("Recommendations",
                new Dictionary<string, string>()
                {
                    { "brand", request.Brand },
                    { "language", request.Language },                    
                    { "industry", request.Industry },
                    { "memory", request.Memory }
                }, connectionId));

            return TypedResults.Ok("Recommendations requested");
        }

    }
}
