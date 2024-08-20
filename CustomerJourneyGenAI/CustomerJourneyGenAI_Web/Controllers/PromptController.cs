using Microsoft.AspNetCore.Mvc;

namespace CustomerJourneyGenAI_Web.Controllers
{
    public class PromptController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
