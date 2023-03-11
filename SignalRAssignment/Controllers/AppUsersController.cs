using Microsoft.AspNetCore.Mvc;
using SignalRAssignment.DataAccess;
using SignalRAssignment.Entity;

namespace SignalRAssignment.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly SignalRDbContext _context;
        private readonly ILogger<AppUsersController> _logger;
        public AppUsersController(SignalRDbContext context, ILogger<AppUsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var users = _context.AppUsers.ToList();
            return View(users);
        }
        public IActionResult Update(int id)
        {
            try
            {
                var user = _context.AppUsers.SingleOrDefault(x => x.UserId == id);
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public IActionResult OnUpdate(AppUsers us)
        {
            try
            {
                _context.AppUsers.Update(us);
                _context.SaveChanges();
                var list = _context.AppUsers.ToList();
                return View("/Views/AppUsers/Index.cshtml", list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public IActionResult OnDelete(int id)
        {
            try
            {
                var user = _context.AppUsers.SingleOrDefault(x => x.UserId == id);
                if (user == null)
                {
                    return NotFound();
                }
                _context.AppUsers.Remove(user);
                _context.SaveChanges();
                var list = _context.AppUsers.ToList();
                return View("/Views/AppUsers/Index.cshtml", list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult OnAdd(AppUsers us)
        {
            try
            {
                _context.AppUsers.Add(us);
                _context.SaveChanges();
                var list = _context.AppUsers.ToList();
                return View("/Views/AppUsers/Index.cshtml", list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
