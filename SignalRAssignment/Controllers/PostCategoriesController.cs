using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.DataAccess;
using SignalRAssignment.Entity;

namespace SignalRAssignment.Controllers
{
    public class PostCategoriesController : Controller
    {
        private readonly SignalRDbContext _context;
        private readonly ILogger<PostCategoriesController> _logger;
        public PostCategoriesController(SignalRDbContext context, ILogger<PostCategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var list = _context.PostCategories.ToList();
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
        public IActionResult OnUpdate(PostCategories po)
        {
            try
            {
                _context.PostCategories.Update(po);
                _context.SaveChanges();
                var list = _context.PostCategories.ToList();
                return View("/Views/PostCategories/Index.cshtml", list);
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
                var post = _context.PostCategories.SingleOrDefault(x => x.CategoryId == id);
                if (post == null)
                {
                    return NotFound();
                }
                _context.PostCategories.Remove(post);
                _context.SaveChanges();
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
        public IActionResult OnAdd(PostCategories po)
        {
            try
            {
                _context.PostCategories.Add(po);
                _context.SaveChanges();
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
