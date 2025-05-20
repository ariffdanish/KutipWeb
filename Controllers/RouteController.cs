using Microsoft.AspNetCore.Mvc;

namespace KutipWeb.Controllers
{
    public class RouteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
