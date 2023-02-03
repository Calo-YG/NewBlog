using Calo.Blog.EntityCore.DataBase.Entities;
using Calo.Blog.EntityCore.DataBase.Repository;
using Calo.Blog.Extenions.Attributes;
using Calo.Blog.Host.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Calo.Blog.Host.Controllers
{
    [NoResult]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBaseRepository<User, long> userRespo;

        public HomeController(ILogger<HomeController> logger, IBaseRepository<User, long> baseRepository)
        {
            _logger = logger;
            userRespo = baseRepository;
        }
        public IActionResult Index()
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