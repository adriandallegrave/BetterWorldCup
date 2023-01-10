using Better.Application.Interfaces;
using Better.Controllers.Base;
using Better.Models;
using Better.Tools.Configuration;
using Better.Tools.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Better.Controllers.Web
{
    public class HomeController : BaseWebController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebAppService _webAppService;

        public HomeController(ILogger<HomeController> logger, IWebAppService webAppService)
        {
            _logger = logger;
            _webAppService = webAppService;
        }

        public IActionResult Index()
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));
            var tableItems = _webAppService.GenerateHomeTable().Result;
            var users = _webAppService.GenerateUsers().Result;
            var table = new HomeViewModel()
            {
                Users = users,
                TableItems = tableItems,
                Version = Constants.Version
            };

            return View(table);
        }

        public IActionResult Complaints()
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
