using Microsoft.AspNetCore.Mvc;

namespace SAV.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
