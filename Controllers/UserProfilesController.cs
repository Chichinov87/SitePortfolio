using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SytePortfolio.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly PortfolioContext _context;
        public UserProfilesController(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            FullProfile model = new FullProfile();

            var portfolioHome = from profile in _context.UserProfile
                                join image in _context.ImgUser on profile.IdUser equals image.IdUser
                                where image.IdImg == profile.MainImg

                                select new FullProfile
                                {
                                    IdUser = profile.IdUser,
                                    NameUser = profile.NameUser,
                                    SurnameUser = profile.SurnameUser,
                                    CityUser = profile.AdressUser,
                                    MainImg = profile.MainImg,
                                    IdImg = image.IdImg,
                                    ImgUser = image.DataImg
                                };
            model.User = portfolioHome.ToList();
            return View(model);

        }


        public async Task<IActionResult> MyProfile()
        {
            int id = 0;
            string statusPassword="";
            if (Request.Cookies.ContainsKey("IdUser") && Request.Cookies.ContainsKey("UserPassword"))
            {
                id = int.Parse(Request.Cookies["IdUser"]);
                statusPassword = Request.Cookies["UserPassword"];
            }

            if (statusPassword == "true" && id > 0)
            {
                var userProfile = await _context.UserProfile.FirstOrDefaultAsync(m => m.IdUser == id);
                //if (userProfile == null)
                //{
                //    return NotFound();
                //}

                PortfolioContext db = new PortfolioContext();
                var img = db.ImgUser.Where(m => m.IdUser == id);
                var educationUser = await _context.Education.FirstOrDefaultAsync(m => m.IdUser == id);
                var userServices = await _context.UserServices.FirstOrDefaultAsync(m => m.IdUser == id);
                var userWorkplace = await _context.UserWorkplace.FirstOrDefaultAsync(m => m.IdUser == id);
                var userBiography = await _context.UserBiography.FirstOrDefaultAsync(m => m.IdUser == id);

                FullUser model = new FullUser();
                model.Image = img;
                model.OneUser = userProfile;
                model.EducationUser = educationUser;
                model.ServicesUser = userServices;
                model.WorkplaceUser = userWorkplace;
                model.BiographyUser = userBiography;
                return View(model);
                //return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile.FirstOrDefaultAsync(m => m.IdUser == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            PortfolioContext db = new PortfolioContext();
            var img = db.ImgUser.Where(m => m.IdUser == id);
            var educationUser = await _context.Education.FirstOrDefaultAsync(m => m.IdUser == id);
            var userServices = await _context.UserServices.FirstOrDefaultAsync(m => m.IdUser == id);
            var userWorkplace = await _context.UserWorkplace.FirstOrDefaultAsync(m => m.IdUser == id);
            var userBiography = await _context.UserBiography.FirstOrDefaultAsync(m => m.IdUser == id);

            FullUser model = new FullUser();
            model.OneUser = userProfile;
            model.Image = img;
            model.EducationUser = educationUser;
            model.ServicesUser = userServices;
            model.WorkplaceUser = userWorkplace;
            model.BiographyUser = userBiography;
            return View(model);
        }

        // GET: UserProfiles/Edit/5
        public async Task<IActionResult> Edit()
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

                FullUser model = new FullUser();
                model.OneUser = await _context.UserProfile.FindAsync(id);
                model.EducationUser = await _context.Education.FindAsync(id);
                model.ServicesUser = await _context.UserServices.FindAsync(id);
                model.WorkplaceUser = await _context.UserWorkplace.FindAsync(id);
                model.BiographyUser = await _context.UserBiography.FindAsync(id);
                model.Image = _context.ImgUser.Where(m => m.IdUser == id);
                if (model.OneUser == null)
                {
                    return NotFound();
                }
                return View(model);
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FullUser fullUser, IFormFileCollection files)
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
               

                if (id != fullUser.OneUser.IdUser)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    if (fullUser.BiographyUser != null)
                        fullUser.BiographyUser.IdUser = id;
                        _context.Update(fullUser.BiographyUser);
                    if (fullUser.EducationUser != null)
                        fullUser.EducationUser.IdUser = id;
                        _context.Update(fullUser.EducationUser);
                    if (fullUser.OneUser != null)
                        _context.Update(fullUser.OneUser);
                    if (fullUser.ServicesUser != null)
                        fullUser.ServicesUser.IdUser = id;
                        _context.Update(fullUser.ServicesUser);
                    if (fullUser.WorkplaceUser != null)
                        fullUser.WorkplaceUser.IdUser = id;
                        _context.Update(fullUser.WorkplaceUser);

                        await _context.SaveChangesAsync();
                }
                else return View(fullUser);

                ImgUser img = new ImgUser();

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
                        img.IdUser = id;
                        img.IdImg = 0;
                        img.DataImg = imageData;
                        img.TypeImg = type;

                        _context.Add(img);
                    }
                    await _context.SaveChangesAsync();

                    int lastIdImg = _context.ImgUser.Where(p => p.IdUser == idCookie).Max(p => p.IdImg);
                    var profile = _context.UserProfile.Find(idCookie);
                    profile.MainImg = lastIdImg;
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                return RedirectToRoute(new
                {
                    controller = "UserProfiles",
                    action = "Details",
                    id = id
                });

            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }


        [HttpPost]
        public ActionResult UploadImage()
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
                var img = _context.ImgUser.Where(m => m.IdUser == idCookie);
                if(img != null)
                {
                    FullUser model = new FullUser();
                    model.Image = img.ToList();
                    return PartialView(model);
                }
                ViewBag.Message = "Это частичное представление";
                return PartialView();
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
                var imgProfile = await _context.ImgUser.FindAsync(id);
                _context.ImgUser.Remove(imgProfile);
                await _context.SaveChangesAsync();

                return View();
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }




            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile
                .FirstOrDefaultAsync(m => m.IdUser == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfile = await _context.UserProfile.FindAsync(id);
            var imgProfile = await _context.ImgUser.FindAsync(id);
            var educationProfile = await _context.Education.FindAsync(id);
            var workplaceProfile = await _context.UserWorkplace.FindAsync(id);
            var servicesProfile = await _context.UserServices.FindAsync(id);
            var biographyProfile = await _context.UserBiography.FindAsync(id);
            var authorizationProfile = await _context.UserAuthorization.FindAsync(id);
            _context.UserProfile.Remove(userProfile);
            _context.Education.Remove(educationProfile);
            _context.UserWorkplace.Remove(workplaceProfile);
            _context.UserServices.Remove(servicesProfile);
            _context.UserBiography.Remove(biographyProfile);
            _context.UserAuthorization.Remove(authorizationProfile);
            while (await _context.ImgUser.FirstOrDefaultAsync(i => i.IdUser == id) != null)
            {
                var img = await _context.ImgUser.FirstOrDefaultAsync(i => i.IdUser == id);
                _context.ImgUser.Remove(img);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            Response.Cookies.Append("UserPassword", " ");
            Response.Cookies.Append("LoginUser", " ");
            Response.Cookies.Append("IdUser", " ");
            return Redirect("/UserAuthorization/Create");

        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfile.Any(e => e.IdUser == id);
        }
    }
}
