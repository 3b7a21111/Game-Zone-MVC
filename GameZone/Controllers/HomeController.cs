using GameZone.Models;
using GameZone.Services.Game_Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
		private readonly IGameService gameService;

		public HomeController(IGameService gameService)
        {
			this.gameService = gameService;
		}
        public IActionResult Index()
        {
            var games = gameService.GetAll();
            return View(games);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}