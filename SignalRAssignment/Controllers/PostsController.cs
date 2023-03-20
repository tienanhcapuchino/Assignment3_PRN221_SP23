using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.DataAccess;
using SignalRAssignment.Entity;
using SignalRAssignment.Hubs;
using SignalRAssignment.Models;

namespace SignalRAssignment.Controllers
{
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;
        private readonly SignalRDbContext _rdbContext;
        private readonly IHubContext<SignalRServer> _signalRHub;
        private readonly int TotalItemsInPage = 2;
        public PostsController(ILogger<PostsController> logger, 
            SignalRDbContext rdbContext,
            IHubContext<SignalRServer> hubContext
            )
        {
            _logger = logger;
            _rdbContext = rdbContext;
            _signalRHub = hubContext;
        }

        public async Task<IActionResult> Index(int page = 1, string? seachValue = null)
        {
            try
            {
                var list = await _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToListAsync();
                if (!String.IsNullOrEmpty(seachValue))
                {
                    list = list.Where(x => x.Title.ToLower().Contains(seachValue.ToLower())
                    || x.Content.ToLower().Contains(seachValue.ToLower()))
                        .ToList();
                }
                ViewBag.search = seachValue;
                ViewBag.page = page;
                int totalPage = list.Count % TotalItemsInPage == 0 ?
                    list.Count / TotalItemsInPage :
                    (list.Count / TotalItemsInPage) + 1;
                ViewBag.totalPage = totalPage;
                list = list.Skip(page * TotalItemsInPage).Take(TotalItemsInPage).ToList();
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        //[HttpGet]
        //public IActionResult GetPost()
        //{
        //    var list = _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToList();
        //    return Ok(list);
        //}
        //public IActionResult Search(string seachValue)
        //{
        //    try
        //    {
        //        List<Posts> result = new List<Posts>();
        //        if (String.IsNullOrEmpty(seachValue))
        //        {
        //            result = _rdbContext.Posts.
        //        Include(x => x.AppUsers).
        //        Include(x => x.PostCategories).ToList();
        //        }
        //        else
        //        {
        //            result = _rdbContext.Posts.Where(x => x.PublishStatus == 1
        //        && (x.Title.Contains(seachValue)
        //        || x.Content.Contains(seachValue))).
        //        Include(x => x.AppUsers).
        //        Include(x => x.PostCategories).
        //        ToList();
        //        }
        //        ViewBag.search = seachValue;
        //        return View("/Views/Posts/Index.cshtml", result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        throw;
        //    }
        //}
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
        public async Task <IActionResult> OnUpdate(Posts po)
        {
            try
            {
                var categories = await _rdbContext.PostCategories.ToListAsync();
                ViewBag.cate = categories;
                var authors = _rdbContext.AppUsers.ToList();
                ViewBag.author = authors;
                _rdbContext.Posts.Update(po);
                await _rdbContext.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPosts");
                var list = _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToList();
                return View("/Views/Posts/Index.cshtml", list);
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
                var post = await _rdbContext.Posts.SingleOrDefaultAsync(x => x.PostID == id);
                if (post == null)
                {
                    return NotFound();
                }
                _rdbContext.Posts.Remove(post);
                await _rdbContext.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPosts");
                var list = await _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToListAsync();
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
        public async Task<IActionResult> OnAdd(Posts po)
        {
            try
            {
                var categories = await _rdbContext.PostCategories.ToListAsync();
                ViewBag.cate = categories;
                var authors = await _rdbContext.AppUsers.ToListAsync();
                ViewBag.author = authors;
                _rdbContext.Posts.Add(po);
                await _rdbContext.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPosts");
                var list = await _rdbContext.Posts.Include(x => x.AppUsers).Include(x => x.PostCategories).ToListAsync();
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
