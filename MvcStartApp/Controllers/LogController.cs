using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcStartApp.Models;
using MvcStartApp.Models.Db;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStartApp.Controllers
{
    public class LogController : Controller
    {

        private readonly ILogRepository _repo;
        //private readonly ILogger<HomeController> _logger;

        public LogController(ILogger<HomeController> logger, ILogRepository repo)
        {
            //_logger = logger;
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _repo.GetRequests();
            return View(requests);
        }

        public async Task<IActionResult> AddRequest(Request newRequest)
        {
            await _repo.AddRequest(newRequest);
            return View();
        }

    }
}
