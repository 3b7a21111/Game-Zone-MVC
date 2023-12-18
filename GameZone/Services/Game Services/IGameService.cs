using GameZone.Models;
using GameZone.ViewModel;

namespace GameZone.Services.Game_Services
{
	public interface IGameService
	{
		 Task Create(CreateGameFormWithViewModel createGameVM);
		IEnumerable<Game> GetAll();
		Game? GetById(int id);
		Task<Game?> Edit(EditGameFormWithViewModel Editgame);
		bool Delete(int id);
	}
}
