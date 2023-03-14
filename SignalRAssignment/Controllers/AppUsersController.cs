using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.DataAccess;
using SignalRAssignment.Entity;
using SignalRAssignment.Hubs;

namespace SignalRAssignment.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly SignalRDbContext _context;
        private readonly ILogger<AppUsersController> _logger;
        private readonly IHubContext<SignalRServer> _signalRHub;
        public AppUsersController(SignalRDbContext context, 
            ILogger<AppUsersController> logger,
            IHubContext<SignalRServer> signalRHub)
        {
            _context = context;
            _logger = logger;
            _signalRHub = signalRHub;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.AppUsers.ToListAsync();
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
        public async Task<IActionResult> OnUpdate(AppUsers us)
        {
            try
            {
                _context.AppUsers.Update(us);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadApp");
                var list = _context.AppUsers.ToList();
                return View("/Views/AppUsers/Index.cshtml", list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<IActionResult> OnDelete(int id)
        {
            try
            {
                var user = await _context.AppUsers.SingleOrDefaultAsync(x => x.UserId == id);
                if (user == null)
                {
                    return NotFound();
                }
                _context.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadApp");
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
        public async Task<IActionResult> OnAdd(AppUsers us)
        {
            try
            {
                _context.AppUsers.Add(us);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadApp");
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
