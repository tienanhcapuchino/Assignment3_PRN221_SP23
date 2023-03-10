using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.DataAccess;

namespace SignalRAssignment.Controllers
{
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;
        private readonly SignalRDbContext _rdbContext;
        public PostsController(ILogger<PostsController> logger, SignalRDbContext rdbContext)
        {
            _logger = logger;
            _rdbContext = rdbContext;
        }

        public IActionResult Index()
        {
            try
            {
                var list = _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToList();
                return View(list);
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
                var list = _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToList();
                var post = _rdbContext.Posts.SingleOrDefault(x => x.PostID == id);
                _rdbContext.Posts.Remove(post);
                _rdbContext.SaveChanges();
                return View("/Views/Posts/Index.cshtml", list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public IActionResult Add()
        {
            return View("/Views/Posts/Add.cshtml");
        }
    }
}
