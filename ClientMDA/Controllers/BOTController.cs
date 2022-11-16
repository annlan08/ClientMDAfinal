using ClientMDA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace ClientMDA.Controllers
{
    public class BOTController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly MDAContext _MDA;

        Random rd = new Random();

        public BOTController(ILogger<ApiController> logger, MDAContext MDA)
        {
            _logger = logger;
            _MDA = MDA;
            _MDA.電影圖片movieIimagesLists.ToList();
            _MDA.電影圖片總表movieImages.ToList();
            _MDA.電影movies.ToList();
            _MDA.我的片單myMovieLists.ToList();
        }
        public IActionResult BOT()
        {
            return View();
        }

      
    }
}
