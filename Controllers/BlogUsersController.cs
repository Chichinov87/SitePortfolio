using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SytePortfolio.Controllers
{
    public class BlogUsersController : Controller
    {
        private readonly PortfolioContext _context;
        public BlogUsersController(PortfolioContext context)
        {
            _context = context;
        }

        // GET: BlogUsers
        public async Task<IActionResult> Index()
        {
            var db = new PortfolioContext();
            var image = db.ImgBlog.ToList();
            var blog = db.BlogUser.ToList();
            var model = new FullBlog { Image = image, Blog = blog };
            return View(model);
        }

        public async Task<IActionResult> HisBlogs(int? id)
        {
            var blogUser = _context.BlogUser.Where(m => m.IdUser == id);
            var img = _context.ImgBlog.Where(m => m.IdUser == id);
            if (blogUser == null)
            {
                return NotFound();
            }


            FullBlog model = new FullBlog();
            model.Blog = blogUser.ToList();
            if (img != null)
                model.Image = img.ToList();
            return View(model);
        }

        public async Task<IActionResult> MyBlogs()
        {
            int id = 0;
            string statusPassword = "";
            if (Request.Cookies.ContainsKey("IdUser") && Request.Cookies.ContainsKey("UserPassword"))
            {
                id = int.Parse(Request.Cookies["IdUser"]);
                statusPassword = Request.Cookies["UserPassword"];
            }

            if (statusPassword == "true" && id > 0)
            {

                var blogUser = _context.BlogUser.Where(m => m.IdUser == id);
                var img = _context.ImgBlog.Where(m => m.IdUser == id);
                if (blogUser == null)
                {
                    return NotFound();
                }
                

                FullBlog model = new FullBlog();
                model.Blog = blogUser.ToList();
                if(img != null)
                model.Image = img.ToList();
                return View(model);
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }

        // GET: BlogUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var blogUser = await _context.BlogUser
                .FirstOrDefaultAsync(m => m.IdBlog == id);
            if (blogUser == null)
            {
                return NotFound();
            }
            PortfolioContext db = new PortfolioContext();
            var img = db.ImgBlog.Where(m => m.IdBlog == id);

            FullBlog model = new FullBlog();
            model.OneBlog = blogUser;
            model.Image = img;
            return View(model);
        }

        // GET: BlogUsers/Create
        public IActionResult Create()
        {
            int id = 0;
            string statusPassword = "";
            if (Request.Cookies.ContainsKey("IdUser") && Request.Cookies.ContainsKey("UserPassword"))
            {
                id = int.Parse(Request.Cookies["IdUser"]);
                statusPassword = Request.Cookies["UserPassword"];
            }

            if (statusPassword == "true" && id > 0)
            {
                return View();
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogUser blogUser, IFormFileCollection files)
        {
            int id = 0;
            string statusPassword = "";
            if (Request.Cookies.ContainsKey("IdUser") && Request.Cookies.ContainsKey("UserPassword"))
            {
                id = int.Parse(Request.Cookies["IdUser"]);
                statusPassword = Request.Cookies["UserPassword"];
            }

            if (statusPassword == "true" && id > 0)
            {
                if (ModelState.IsValid)
                {
                    blogUser.DateBlog = DateTime.Now;
                    blogUser.IdUser = id;
                    _context.Add(blogUser);
                    await _context.SaveChangesAsync();
                }
                else return View(blogUser);

                ImgBlog img = new ImgBlog();

                int count = 0;
                count = _context.BlogUser.Max(p => p.IdBlog);

                foreach (var file in files)
                {
                    if (file != null)
                    {
                        byte[] imageData = null;

                        using (var fileStream = file.OpenReadStream())
                        using (var ms = new MemoryStream())
                        {
                            fileStream.CopyTo(ms);
                            imageData = ms.ToArray();
                        }

                        string type = file.ContentType;
                        img.IdBlog = count;
                        img.IdImg = 0;
                        img.DataImg = imageData;
                        img.IdUser = id;
                        img.TypeImg = type;

                        _context.Add(img);
                    }
                    await _context.SaveChangesAsync();

                    int lastIdImg = _context.ImgBlog.Max(p => p.IdImg);
                    int lastBlog = _context.BlogUser.Max(p => p.IdBlog);
                    var blog = _context.BlogUser.Find(lastBlog);
                    blog.MainImg = lastIdImg;
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
                
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }



        public async Task<ActionResult> DeleteImage(int? id)
        {
            int idCookie = 0;
            string statusPassword = "";
            if (Request.Cookies.ContainsKey("IdUser") && Request.Cookies.ContainsKey("UserPassword"))
            {
                idCookie = int.Parse(Request.Cookies["IdUser"]);
                statusPassword = Request.Cookies["UserPassword"];
            }

            if (statusPassword == "true" && idCookie > 0)
            {
                var imgBlog = await _context.ImgBlog.FindAsync(id);
                _context.ImgBlog.Remove(imgBlog);
                await _context.SaveChangesAsync();
                return View();
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }
        //******************************************************************************

        public async Task<IActionResult> Edit(int? id)
        {
            int idCookie = 0;
            string statusPassword = "";
            if (Request.Cookies.ContainsKey("IdUser") && Request.Cookies.ContainsKey("UserPassword"))
            {
                idCookie = int.Parse(Request.Cookies["IdUser"]);
                statusPassword = Request.Cookies["UserPassword"];
            }

            if (statusPassword == "true" && idCookie > 0)
            {

                var blogUser = await _context.BlogUser.FindAsync(id);
                if (blogUser == null)
                {
                    return NotFound();
                }
                PortfolioContext db = new PortfolioContext();
                var img = db.ImgBlog.Where(m => m.IdBlog == id);

                FullBlog model = new FullBlog();
                ViewBag.Type = model.GetType();
                model.OneBlog = blogUser;
                model.Image = img;
                return View(model);
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FullBlog fullBlog, IFormFileCollection files)
        {
            int idCookie = 0;
            string statusPassword = "";
            if (Request.Cookies.ContainsKey("IdUser") && Request.Cookies.ContainsKey("UserPassword"))
            {
                idCookie = int.Parse(Request.Cookies["IdUser"]);
                statusPassword = Request.Cookies["UserPassword"];
            }

            if (statusPassword == "true" && idCookie > 0)
            {

                if (ModelState.IsValid)
                {
                    if (fullBlog.OneBlog != null)
                        fullBlog.OneBlog.IdUser = idCookie;
                    _context.Update(fullBlog.OneBlog);
                    await _context.SaveChangesAsync();
                }
                else return View(fullBlog);

                ImgBlog img = new ImgBlog();
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            byte[] imageData = null;

                            using (var fileStream = file.OpenReadStream())
                            using (var ms = new MemoryStream())
                            {
                                fileStream.CopyTo(ms);
                                imageData = ms.ToArray();
                            }


                            string type = file.ContentType;
                            img.IdUser = idCookie;
                            img.IdBlog = fullBlog.OneBlog.IdBlog;
                            img.IdImg = 0;
                            img.DataImg = imageData;
                            img.TypeImg = type;

                            _context.Add(img);
                        }
                        await _context.SaveChangesAsync();

                        int lastIdImg = _context.ImgBlog.Where(p=>p.IdBlog == fullBlog.OneBlog.IdBlog).Max(p => p.IdImg);
                        var blog = _context.BlogUser.Find(fullBlog.OneBlog.IdBlog);
                        blog.MainImg = lastIdImg;
                        _context.Update(blog);
                        await _context.SaveChangesAsync();
                    }
                }
                else Redirect("/Home/Index");
                return RedirectToRoute(new
                {
                    controller = "BlogUsers",
                    action = "Details",
                    id = fullBlog.OneBlog.IdBlog
                });

            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }



        public async Task<IActionResult> Delete(int? id)
        {
            int idCookie = 0;
            string statusPassword = "";
            if (Request.Cookies.ContainsKey("IdUser") && Request.Cookies.ContainsKey("UserPassword"))
            {
                idCookie = int.Parse(Request.Cookies["IdUser"]);
                statusPassword = Request.Cookies["UserPassword"];
            }

            if (statusPassword == "true" && idCookie > 0)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var blogUser = await _context.BlogUser
                    .FirstOrDefaultAsync(m => m.IdBlog == id);
                if (blogUser == null)
                {
                    return NotFound();
                }

                return View(blogUser);
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            int idCookie = 0;
            string statusPassword = "";
            if (Request.Cookies.ContainsKey("IdUser") && Request.Cookies.ContainsKey("UserPassword"))
            {
                idCookie = int.Parse(Request.Cookies["IdUser"]);
                statusPassword = Request.Cookies["UserPassword"];
            }

            if (statusPassword == "true" && idCookie > 0)
            {
                var blogUser = await _context.BlogUser.FindAsync(id);
                _context.BlogUser.Remove(blogUser);
                await _context.SaveChangesAsync();
                while (await _context.ImgBlog.FirstOrDefaultAsync(i => i.IdBlog == id) != null)
                {
                    var img = await _context.ImgBlog.FirstOrDefaultAsync(i => i.IdBlog == id);
                    _context.ImgBlog.Remove(img);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }

        private bool BlogUserExists(int id)
        {
            return _context.BlogUser.Any(e => e.IdBlog == id);
        }
    }
}
