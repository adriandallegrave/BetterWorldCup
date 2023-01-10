using Better.Application.Interfaces;
using Better.Application.Objects;
using Better.Controllers.Base;
using Better.Models;
using Better.Tools.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Better.Controllers.Web
{
    public class BetsController : BaseWebController
    {
        private readonly ILogger<BetsController> _logger;
        private readonly IWebAppService _webAppService;

        public BetsController(ILogger<BetsController> logger, IWebAppService webAppService)
        {
            _logger = logger;
            _webAppService = webAppService;
        }

        [HttpGet]
        public IActionResult Index(string error = "")
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            var table = new BetsViewModel()
            {
                UserMail = User.Identity.Name,
                UserHaveBets = true,
                TableItems = new List<BetsTableItem>(),
                Bets = new List<BetSelection>(),
                Error = error
            };
            
            if (!_webAppService.UserHaveBets(table.UserMail).Result)
            {
                table.UserHaveBets = false;
                table.TableItems = _webAppService.GenerateBetsTable().Result;
                table.Bets = _webAppService.GenerateBets().Result;
            }

            return View(table);
        }
        
        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            var betsSaved = _webAppService.SaveBets(form).Result;

            if (betsSaved.Item1)
            {
                return Redirect("/Home");
            }

            return Index(betsSaved.Item2);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
