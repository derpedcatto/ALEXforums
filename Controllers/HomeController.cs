﻿using ALEXforums.Data;
using ALEXforums.Models;
using ALEXforums.Services.Hash;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult ForumPost()
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