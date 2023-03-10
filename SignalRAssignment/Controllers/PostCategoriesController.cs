using Microsoft.AspNetCore.Mvc;

namespace SignalRAssignment.Controllers
{
    public class PostCategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
