using ALEXforums.Data;
using ALEXforums.Data.Entity;
using ALEXforums.Models;
using ALEXforums.Models.Forum;
using ALEXforums.Models.Home;
using ALEXforums.Services.Hash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ALEXforums.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHashService _hashService;
        private readonly DataContext _dataContext;

        public HomeController(ILogger<HomeController> logger, IHashService hashService, DataContext dataContext)
        {
            _logger = logger;
            _hashService = hashService;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            /*
            if (model == null)
            {
                model = new();
                model.PostCategories = _dataContext.ForumCategories.Select(
                                            category => new SelectListItem
                                            {
                                                Value = category.Name,
                                                Text = category.Name
                                            }).ToList();
            }
            */
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}