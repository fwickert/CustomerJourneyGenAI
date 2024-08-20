using CustomerJourneyGenAI.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerJourneyGenAI_Web.Controllers
{
    public class MarketingController : Controller
    {

        private const string COMPANY = "Luxor";

        private const string LANGUAGE = "French";

        private const string INDUSTRY = "Watch and jewelry";

        private readonly ILogger<HomeController> _logger;

        public MarketingController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MarketingViewModel model = new MarketingViewModel();
            model.Company = COMPANY;
            model.Product = INDUSTRY;
            model.Language = LANGUAGE;

            return View(model);
        }




    }
}
