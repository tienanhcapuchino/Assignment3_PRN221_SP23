using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.DataAccess;
using SignalRAssignment.Entity;

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
        public IActionResult Search(string seachValue)
        {
            try
            {
                List<Posts> result = new List<Posts>();
                if (String.IsNullOrEmpty(seachValue))
                {
                    result = _rdbContext.Posts.
                Include(x => x.AppUsers).
                Include(x => x.PostCategories).ToList();
                }
                else
                {
                    result = _rdbContext.Posts.Where(x => x.PublishStatus == 1
                && (x.Title.Contains(seachValue)
                || x.Content.Contains(seachValue))).
                Include(x => x.AppUsers).
                Include(x => x.PostCategories).
                ToList();
                }
                ViewBag.search = seachValue;
                return View("/Views/Posts/Index.cshtml", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public IActionResult Update(int id)
        {
            try
            {
                var categories = _rdbContext.PostCategories.ToList();
                ViewBag.cate = categories;
                var authors = _rdbContext.AppUsers.ToList();
                ViewBag.author = authors;
                var post = _rdbContext.Posts.SingleOrDefault(x => x.PostID == id);
                return View(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public IActionResult OnUpdate(Posts po)
        {
            try
            {
                var categories = _rdbContext.PostCategories.ToList();
                ViewBag.cate = categories;
                var authors = _rdbContext.AppUsers.ToList();
                ViewBag.author = authors;
                _rdbContext.Posts.Update(po);
                _rdbContext.SaveChanges();
                var list = _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToList();
                return View("/Views/Posts/Index.cshtml", list);
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
                var post = _rdbContext.Posts.SingleOrDefault(x => x.PostID == id);
                if (post == null)
                {
                    return NotFound();
                }
                _rdbContext.Posts.Remove(post);
                _rdbContext.SaveChanges();
                var list = _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToList();
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
            var categories = _rdbContext.PostCategories.ToList();
            ViewBag.cate = categories;
            var authors = _rdbContext.AppUsers.ToList();
            ViewBag.author = authors;
            return View("/Views/Posts/Add.cshtml");
        }
        public IActionResult OnAdd(Posts po)
        {
            try
            {
                var categories = _rdbContext.PostCategories.ToList();
                ViewBag.cate = categories;
                var authors = _rdbContext.AppUsers.ToList();
                ViewBag.author = authors;
                _rdbContext.Posts.Add(po);
                _rdbContext.SaveChanges();
                var list = _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToList();
                return View("/Views/Posts/Index.cshtml", list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
