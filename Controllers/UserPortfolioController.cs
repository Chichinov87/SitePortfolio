using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SytePortfolio.Controllers
{
    public class UserPortfolioController : Controller
    {
        private readonly PortfolioContext _context;
        public UserPortfolioController(PortfolioContext context)
        {
            _context = context;
        }

        // GET: UserPortfolio
        public async Task<IActionResult> Index()
        {
            var db = new PortfolioContext();
            var image = db.ImgPortfolio.ToList();
            var user = db.UserPortfolio.ToList();
            var model = new FullPortfolio { Image = image, User = user };
            return View(model);
        }

        public async Task<IActionResult> HisPortfolio(int? id)
        {
            FullPortfolio model = new FullPortfolio();
            var userPortfolio = _context.UserPortfolio.Where(m => m.IdUser == id);
            var img = _context.ImgPortfolio.Where(m => m.IdUser == id);
            if (userPortfolio == null)
            {
                return NotFound();
            }

            model.User = userPortfolio.ToList();
            if (img != null)
                model.Image = img.ToList();
            return View(model);
        }


        public async Task<IActionResult> MyPortfolios()
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
                FullPortfolio model = new FullPortfolio();
                var userPortfolio = _context.UserPortfolio.Where(m => m.IdUser == id);
                var img = _context.ImgPortfolio.Where(m => m.IdUser == id);
                if (userPortfolio == null)
                {
                    return NotFound();
                }
                
                model.User = userPortfolio.ToList();
                if(img != null)
                model.Image = img.ToList();
                return View(model);
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }
    

    // GET: UserPortfolio/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var userPortfolio = await _context.UserPortfolio
                .FirstOrDefaultAsync(m => m.IdPortfolio == id);
            var userProfile = await _context.UserProfile
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (userPortfolio == null)
            {
                return NotFound();
            }
            PortfolioContext db = new PortfolioContext();
            var img = db.ImgPortfolio.Where(m => m.IdPortfolio == id);
            

            FullPortfolio model = new FullPortfolio();
            model.OnePortfolio = userPortfolio;
            model.OneProfile = userProfile;
            model.Image = img;
            return View(model);
        }

        // GET: UserPortfolio/Create
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
        public async Task<IActionResult> Create(UserPortfolio userPortfolio, IFormFileCollection files)
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
                    userPortfolio.DatePortfolio = DateTime.Now;
                    userPortfolio.IdUser = id;
                    _context.Add(userPortfolio);
                    await _context.SaveChangesAsync();
                }
                else return View(userPortfolio);

                ImgPortfolio img = new ImgPortfolio();

                int count = 0;
                count = _context.UserPortfolio.Max(p => p.IdPortfolio);

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
                        img.IdPortfolio = count;
                        img.IdImg = 0;
                        img.DataImg = imageData;
                        img.IdUser = id;
                        img.TypeImg = type;

                        _context.Add(img);
                    }
                    await _context.SaveChangesAsync();
                    int lastIdImg = _context.ImgPortfolio.Max(p => p.IdImg);
                    int lastPortfolio = _context.UserPortfolio.Max(p => p.IdPortfolio);
                    var portfolio = _context.UserPortfolio.Find(lastPortfolio);
                    portfolio.MainImg = lastIdImg;
                    _context.Update(portfolio);
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
                var imgPortfolio = await _context.ImgPortfolio.FindAsync(id);
                _context.ImgPortfolio.Remove(imgPortfolio);
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

                var userPortfolio = await _context.UserPortfolio.FindAsync(id);
                if (userPortfolio == null)
                {
                    return NotFound();
                }
                PortfolioContext db = new PortfolioContext();
                var img = db.ImgPortfolio.Where(m => m.IdPortfolio == id);

                FullPortfolio model = new FullPortfolio();
                ViewBag.Type = model.GetType();
                model.OnePortfolio = userPortfolio;
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
        public async Task<IActionResult> Edit(FullPortfolio fullPortfolio, IFormFileCollection files)
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
                    if (fullPortfolio.OnePortfolio != null)
                        fullPortfolio.OnePortfolio.IdUser = idCookie;
                    _context.Update(fullPortfolio.OnePortfolio);
                    await _context.SaveChangesAsync();
                }
                else return View(fullPortfolio);


                ImgPortfolio img = new ImgPortfolio();
                
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
                        img.IdPortfolio = fullPortfolio.OnePortfolio.IdPortfolio;
                        img.IdImg = 0;
                        img.DataImg = imageData;
                        img.TypeImg = type;

                        _context.Add(img);
                    }
                    await _context.SaveChangesAsync();


                    var portfolio = _context.UserPortfolio.Find(fullPortfolio.OnePortfolio.IdPortfolio);
                    try
                    {
                        var imgMain = _context.ImgPortfolio.Find(portfolio.MainImg);
                    }
                    catch (NullReferenceException)
                    {
                        var findImg = _context.ImgPortfolio.Where(p => p.IdPortfolio == fullPortfolio.OnePortfolio.IdPortfolio).Max(p => p.IdImg);
                        if (findImg > 1)
                        {
                            portfolio.MainImg = findImg;
                            _context.Update(portfolio);
                            await _context.SaveChangesAsync();
                        }
                        else 
                        {
                            portfolio.MainImg = 2;
                        }
                    }

                }
                return RedirectToRoute(new
                {
                    controller = "UserPortfolio",
                    action = "Details",
                    id = fullPortfolio.OnePortfolio.IdPortfolio
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

                var userPortfolio = await _context.UserPortfolio
                .FirstOrDefaultAsync(m => m.IdPortfolio == id);
                if (userPortfolio == null)
                {
                    return NotFound();
                }

                return View(userPortfolio);
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
                var userPortfolio = await _context.UserPortfolio.FindAsync(id);
                _context.UserPortfolio.Remove(userPortfolio);
                await _context.SaveChangesAsync();

                while (await _context.ImgPortfolio.FirstOrDefaultAsync(i => i.IdPortfolio == id) != null)
                {
                    var img = await _context.ImgPortfolio.FirstOrDefaultAsync(i => i.IdPortfolio == id);
                    _context.ImgPortfolio.Remove(img);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }

        private bool UserPortfolioExists(int id)
        {
            return _context.UserPortfolio.Any(e => e.IdPortfolio == id);
        }
    }
}