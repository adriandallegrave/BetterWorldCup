using Better.Application.Interfaces;
using Better.Controllers.Base;
using Better.Models;
using Better.Tools.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Better.Controllers.Web
{
    public class ResultsController : BaseWebController
    {
        private readonly ILogger<ResultsController> _logger;
        private readonly IWebAppService _webAppService;

        public ResultsController(ILogger<ResultsController> logger, IWebAppService webAppService)
        {
            _logger = logger;
            _webAppService = webAppService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            var resultsTableItens = _webAppService.GenerateResults().Result;

            var table = new ResultsViewModel()
            {
                TableItems = resultsTableItens
            };

            return View(table);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
