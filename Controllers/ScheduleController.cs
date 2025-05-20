using Microsoft.AspNetCore.Mvc;

namespace KutipWeb.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
