using Microsoft.AspNetCore.Mvc;

namespace ALEXforums.Controllers
{
    public class ForumPostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
