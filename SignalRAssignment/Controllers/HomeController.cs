using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.DataAccess;
using SignalRAssignment.Entity;
using SignalRAssignment.Models;
using System.Diagnostics;

namespace SignalRAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignalRDbContext _rdbContext;

        public HomeController(ILogger<HomeController> logger, SignalRDbContext context)
        {
            _logger = logger;
            _rdbContext = context;
        }

        public IActionResult Index()
        {
            try
            {
                var list = _rdbContext.Posts.Where(x => x.PublishStatus == 1).Include(x => x.AppUsers).Include(x => x.PostCategories).ToList();
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IActionResult Search(string seachValue)
        {
            try
            {
                var list = _rdbContext.Posts.Where(x => x.PublishStatus == 1 
                && (x.Title.Contains(seachValue) 
                || x.Content.Contains(seachValue))).
                Include(x => x.AppUsers).
                Include(x => x.PostCategories).
                ToList();
                return View("/Views/Home/Index.cshtml", list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}