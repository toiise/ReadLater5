using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadLater5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Services.Interfaces;
using Services.ServiceModels;

namespace ReadLater5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IClickService _clickService;
        private readonly IBookmarkService _bookmarkService;

        public HomeController(ILogger<HomeController> logger, IClickService clickService, IBookmarkService bookmarkService)
        {
            _logger = logger;
            _clickService = clickService;
            _bookmarkService = bookmarkService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            
            return View();
        }


        [HttpGet]
        public async Task<ActionResult>  MostClicked()
        {
            var clickVm =  await _clickService.GetMostPopularClicks();


            return PartialView("~/Views/Shared/_MostClicked.cshtml", clickVm);

        }

        [HttpGet]
        public async Task<ActionResult> MostClickedToday()
        {
            var clickVm = await _clickService.GetMostPopularClicksToday();


            return PartialView("~/Views/Shared/_TopFiveToday.cshtml", clickVm);

        }

        [HttpPost]
        public async Task<ActionResult> LinksForUser(string userName)
        {
            var clickVm = await _bookmarkService.GetBookmarksByUser(userName);

            return PartialView("~/Views/Shared/_GetBookmarksByUser.cshtml", clickVm);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
