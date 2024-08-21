using CustomerJourneyGenAI.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerJourneyGenAI_Web.Controllers
{
    public class CustomerServiceController : Controller
    {
      

        private readonly ILogger<CustomerServiceController> _logger;

        public CustomerServiceController(ILogger<CustomerServiceController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       

    }
}
