using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice.Controllers
{
    public class DapperPracticeController : Controller
    {
        private readonly ILogger<DapperPracticeController> _logger;
        public DapperPracticeController(ILogger<DapperPracticeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
