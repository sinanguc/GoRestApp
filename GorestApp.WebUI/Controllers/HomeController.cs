using GorestApp.Business.Abstract.UserManager;
using GorestApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GorestApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserReadService _userReadService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(IUserReadService userReadService, ILogger<HomeController> logger)
        {
            _userReadService = userReadService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = _userReadService.GetUserList(new Entities.Concrete.Users.UserFilterModel());

            return View(model);
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
