using Microsoft.AspNetCore.Mvc;

namespace CustomerJourneyGenAI.Web.Controllers
{
    public class CustomerHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
