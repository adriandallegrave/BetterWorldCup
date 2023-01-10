using Better.Controllers.Base;
using Better.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Better.Controllers.Web
{
    public class ExtrasController : BaseWebController
    {
        private readonly ILogger<ExtrasController> _logger;

        public ExtrasController(ILogger<ExtrasController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
