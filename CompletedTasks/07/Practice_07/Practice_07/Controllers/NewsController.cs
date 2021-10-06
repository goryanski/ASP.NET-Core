using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice_07.Models.News;
using Practice_07.UI.Services.Interfaces;

namespace Practice_07.Controllers
{
    public class NewsController : Controller
    {
        INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public async Task<IActionResult> Index()
        {
            return View(new NewsIndexViewModel
            {
                News = await _newsService.GetAllNews()
            });
        }
    }
}
