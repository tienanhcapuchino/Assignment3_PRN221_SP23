using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.DataAccess;
using SignalRAssignment.Entity;
using SignalRAssignment.Hubs;

namespace SignalRAssignment.Controllers
{
    public class PostCategoriesController : Controller
    {
        private readonly SignalRDbContext _context;
        private readonly ILogger<PostCategoriesController> _logger;
        private readonly IHubContext<SignalRServer> _signalRHub;
        public PostCategoriesController(SignalRDbContext context, 
            ILogger<PostCategoriesController> logger,
            IHubContext<SignalRServer> signalRHub)
        {
            _context = context;
            _logger = logger;
            _signalRHub = signalRHub;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.PostCategories.ToListAsync();
            return View(list);
        }
        public IActionResult Update(int id)
        {
            try
            {
                var post = _context.PostCategories.SingleOrDefault(x => x.CategoryId == id);
                return View(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<IActionResult> OnUpdate(PostCategories po)
        {
            try
            {
                _context.PostCategories.Update(po);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPostCate");
                var list = _context.PostCategories.ToList();
                return View("/Views/PostCategories/Index.cshtml", list);
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
                var post = await _context.PostCategories.SingleOrDefaultAsync(x => x.CategoryId == id);
                if (post == null)
                {
                    return NotFound();
                }
                _context.PostCategories.Remove(post);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPostCate");
                var list = _context.PostCategories.ToList();
                return View("/Views/PostCategories/Index.cshtml", list);
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
        public async Task<IActionResult> OnAdd(PostCategories po)
        {
            try
            {
                _context.PostCategories.Add(po);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPostCate");
                var list = _context.PostCategories.ToList();
                return View("/Views/PostCategories/Index.cshtml", list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
