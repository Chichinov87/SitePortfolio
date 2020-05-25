using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SytePortfolio.Service;

namespace SytePortfolio.Controllers
{
    public class UserAuthorizationController : Controller
    {
        private readonly PortfolioContext _context;
        public UserAuthorizationController(PortfolioContext context)
        {
            _context = context;
        }

        // GET: UserAuthorization
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogoutUser()
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
                Response.Cookies.Append("UserPassword", " ");
                Response.Cookies.Append("LoginUser", " ");
                Response.Cookies.Append("IdUser", " ");
                return Redirect("/UserAuthorization");
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entrance(UserAuthorization userAuthorization)
        {
            var dbUser = await _context.UserAuthorization
                .FirstOrDefaultAsync(m =>  m.LoginUser == userAuthorization.LoginUser);

            if(dbUser == null)
            {
                ViewBag.er = "Похоже вы неправильно ввели пароль, попробуйте еще раз";
                return View("Index");
            }

            if (dbUser.HashPassword == userAuthorization.HashPassword && dbUser.LoginUser == userAuthorization.LoginUser)
            {
                Response.Cookies.Append("LoginUser", userAuthorization.LoginUser);
                Response.Cookies.Append("IdUser", (dbUser.IdUser).ToString());
                Response.Cookies.Append("UserPassword", "true");
                //return RedirectToAction("MyProfile", "UserProfiles", new { id=dbUser.IdUser });
                //return Redirect($"/UserProfiles/MyProfile?id={dbUser.IdUser}");
                return RedirectToRoute(new
                {
                    controller = "UserProfiles",
                    action = "MyProfile",
                    id = dbUser.IdUser
                });
            }
            else
            {
                ViewBag.er = "Неправильно введен логин или пароль, регистрация прошла успешно? Вот пароль из базы " + dbUser.HashPassword + "А вот пароль ваш: " + userAuthorization.HashPassword + "вот ваш логин:" + userAuthorization.LoginUser+"а вот логин из базы" + dbUser.LoginUser;
                return View("Index", userAuthorization);
            }


        }
      


        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUser authorization)
        {

            UserAuthorization userAuthorization = new UserAuthorization();
            userAuthorization.LoginUser = authorization.LoginUser;
            userAuthorization.HashPassword = authorization.password1; // GetHash.NewHash(authorization.password1);

            if (ModelState.IsValid)
            {
                _context.Add(new UserProfile {NameUser=" "});
                _context.Add(new Education {EducationalInstitution=" "});
                _context.Add(new UserServices {NameServices=" "});
                _context.Add(new UserWorkplace {UserPosition=" "});
                _context.Add(new UserBiography {UserHistory=" "});
                _context.Add(userAuthorization);
                await _context.SaveChangesAsync();
                await EmailService.SendEmailAsync("89261969132@mail.ru", "Новый пользователь", authorization.LoginUser);

                ViewBag.Nice = "Регистрация прошла успешно, войдите в систему используя свой логин и пароль";
                return View("Index");
            }
            else
            {
                ViewBag.er = "Что то пошло не так!";
                return View();
            }
        }

        // GET: UserAuthorization/Edit/5
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
                CreateUser model = new CreateUser();
                return View(model);
            }
            else
            {
                return Redirect("/UserAuthorization/Index");
            }
        }

   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateUser authorization)
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
                var userAuthorization = await _context.UserAuthorization.FindAsync(idCookie);


                if (userAuthorization.HashPassword == authorization.password)
                {
                    userAuthorization.HashPassword = authorization.password1;
                }
                else
                {
                    ViewBag.er = "Неправильно введен старый пароль";
                    return View();
                }

                _context.Update(userAuthorization);
                await _context.SaveChangesAsync();

                return Redirect("/UserProfiles/Myprofile");
            }
            else
            {
                return Redirect("/UserProfile/Index");
            }
        }


        
    }
}
