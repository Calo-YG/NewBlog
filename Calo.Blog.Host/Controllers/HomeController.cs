using Calo.Blog.EntityCore.DataBase.Entities;
using Calo.Blog.Extenions.Attributes;
using Calo.Blog.Host.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Y.SqlsugarRepository.Repository;

namespace Calo.Blog.Host.Controllers
{
    [NoResult]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBaseRepository<User, Guid> userRespo;

        public HomeController(ILogger<HomeController> logger, IBaseRepository<User, Guid> baseRepository)
        {
            _logger = logger;
            userRespo = baseRepository; 
        }
        public async Task<IActionResult> Index()
        {
             var count = await userRespo.AsQueryable().CountAsync();
             var user = await userRespo.AsQueryable().FirstAsync();
            _logger.LogInformation("用户人数"+count);
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