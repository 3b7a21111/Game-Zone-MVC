using GameZone.Data;
using GameZone.Services;
using GameZone.Services.Device_Repository;
using GameZone.Services.Game_Services;
using GameZone.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
		private readonly ICategories categories;
		private readonly IDevices devices;
		private readonly IGameService gameServices;

		public GamesController(ICategories categories
            , IDevices devices
            ,IGameService gameServices)
        {
			this.categories = categories;
			this.devices = devices;
			this.gameServices = gameServices;
		}
        public IActionResult Index()
        {
            var games = gameServices.GetAll();
            return View(games);
        }
        public IActionResult Details (int id)
        {
            var game = gameServices.GetById(id);
            if(game == null)
            {
                return NotFound();
            }
            return View(game);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormWithViewModel createGameFormWithViewModel = new()
            {
                Categories = categories.GetListOfGategory(),
                Devices = devices.GetListofDevice(),
            };
            return View(createGameFormWithViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (CreateGameFormWithViewModel createGameVM)
        {
            if(!ModelState.IsValid)
            {
                createGameVM.Categories = categories.GetListOfGategory();
                createGameVM.Devices = devices.GetListofDevice();
                return View(createGameVM);
            }

            await gameServices.Create(createGameVM);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit (int id)
        {
            var game =gameServices.GetById (id);
            if(game is null)
            {
                return NotFound();
            }
            EditGameFormWithViewModel EditVM = new()
            {
                Id=game.Id,
                Name=game.Name,
                Description=game.Description,
                CategoryId=game.CategoryId,
                SelectedDevices=game.Games.Select(x=>x.DeviceId).ToList(),
                Categories=categories.GetListOfGategory(),
                Devices=devices.GetListofDevice(),
                CurrentCover=game.Cover,
            };
            return View (EditVM);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditGameFormWithViewModel EditGameVM)
		{
			if (!ModelState.IsValid)
			{
				EditGameVM.Categories = categories.GetListOfGategory();
				EditGameVM.Devices = devices.GetListofDevice();
				return View(EditGameVM);
			}

            var game = await gameServices.Edit(EditGameVM);
            if (game is null)
            {
                return BadRequest();    
            }
			return RedirectToAction(nameof(Index));
		}
        [HttpDelete]
        public IActionResult Delete (int id)
        {
            var isDeleted = gameServices.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
	}
}
