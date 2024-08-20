using CustomerJourney.API.Models;
using CustomerJourney_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerJourney_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptController : ControllerBase
    {
        private readonly PromptService _dashboardService;

        public PromptController(PromptService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> Post([FromBody] Prompt prompt)
        {
            return await _dashboardService.GetPrompt(prompt.FunctionName, prompt.Plugin);
        }
    }
}
