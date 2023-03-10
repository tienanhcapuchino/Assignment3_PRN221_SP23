using Microsoft.AspNetCore.Mvc;

namespace SignalRAssignment.Controllers
{
    public class AppUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
