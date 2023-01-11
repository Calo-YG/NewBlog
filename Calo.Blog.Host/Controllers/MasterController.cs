using Calo.Blog.Extenions.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Calo.Blog.Host.Controllers
{
    [NoResult]
    public class MasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
