using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SytePortfolio.Models;

namespace SytePortfolio.Controllers
{
    public class HomeController : Controller
    {


        private readonly PortfolioContext _context;
        public HomeController(PortfolioContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            FullHome fullHome = new FullHome();

            if (!_context.UserPortfolio.Any() && !_context.UserPortfolio.Any())
            {
                return View("NullData");
            }
            if(!_context.UserPortfolio.Any())
            {
                return View();
            }

            var lastIdP = _context.UserPortfolio.Max(p=>p.IdPortfolio);
            var lastIdB = _context.BlogUser.Max(p=>p.IdBlog);

            var portfolioHome = from portfolio in _context.UserPortfolio where portfolio.IdPortfolio > lastIdP - 4
                                join user in _context.UserProfile on portfolio.IdUser equals user.IdUser
                                join image in _context.ImgPortfolio on portfolio.IdPortfolio equals image.IdPortfolio where image.IdImg == portfolio.MainImg

                                select new PortfolioHome
                                {
                                    IdPortfolio = portfolio.IdPortfolio,
                                    NamePortfolio = portfolio.NamePortfolio,
                                    TypePortfolio = portfolio.TypePortfolio,
                                    DescriptionPortfolio = portfolio.DescriptionPortfolio.Substring(0, 300) + "...",
                                    DatePortfolio = portfolio.DatePortfolio,
                                    IdUserPortfolio = user.IdUser,
                                    NameUserPortfolio = user.NameUser,
                                    ImagePortfolio = image.DataImg
                                };

            var blogHome = from blog in _context.BlogUser
                           where blog.IdBlog > lastIdB - 4
                           join userBlog in _context.UserProfile on blog.IdUser equals userBlog.IdUser
                           join image1 in _context.ImgBlog on blog.IdBlog equals image1.IdBlog where image1.IdImg == blog.MainImg
                           select new BlogHome
                           {
                               IdBlog = blog.IdBlog,
                               NameBlog = blog.NameBlog,
                               TypeBlog = blog.TypeBlog,
                               DescriptionBlog = blog.TextBlog.Substring(0, 400) + "...",
                               DateBlog = blog.DateBlog,
                               IdUserBlog = userBlog.IdUser,
                               NameUserBlog = userBlog.NameUser,
                               ImageBlog = image1.DataImg
                           };

            fullHome.Blog = blogHome.ToList();
            fullHome.Portfolio = portfolioHome.ToList();

            return View(fullHome);

        }

        
        public IActionResult Services()
        {
            FullServices model = new FullServices();
            model.User = _context.UserProfile.ToList();
            model.Services = _context.UserServices.ToList();
            return View(model);
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
