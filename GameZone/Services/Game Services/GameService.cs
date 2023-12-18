using GameZone.Data;
using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services.Game_Services
{
	public class GameService : IGameService
	{

		private readonly ApplicationDbContext context;
		private readonly IWebHostEnvironment webhostenvironment;
		private readonly string imagespath;

		public GameService(ApplicationDbContext context
			, IWebHostEnvironment webhostenvironment)
		{
			this.context = context;
			this.webhostenvironment = webhostenvironment;
			imagespath = $"{webhostenvironment.WebRootPath}{FileSettings.ImagesPath}";
		}
		public async Task Create(CreateGameFormWithViewModel createGameVM)
		{
			var covername = await SaveCover(createGameVM.Cover);
			//stream.Dispose();

			Game game = new()
			{
				Name = createGameVM.Name,
				Description = createGameVM.Description,
				Cover = covername,
				CategoryId = createGameVM.CategoryId,
				Games = createGameVM.SelectedDevices.Select(x => new GameDevice { DeviceId = x }).ToList()
			};

			context.Add(game);
			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var isDeleted = false;
			var game = context.Games.Find(id);

			if(game is null)
			{
				return isDeleted;
			}
			context.Remove(game);
			var effectedRows = context.SaveChanges();
			if (effectedRows > 0)
			{
				isDeleted = true;
				var cover = Path.Combine(imagespath, game.Cover);
				File.Delete(cover);
			}
			return isDeleted;
		}

		public async Task<Game?> Edit(EditGameFormWithViewModel Editgame)
		{
			var game = context.Games
				.Include(x=>x.Games)
				.SingleOrDefault(x=>x.Id==Editgame.Id);

			var hasnewcover = Editgame.Cover is not null;

			var oldimage = game?.Cover;

			if (game is null)
			{
				return null;
			}

			game.Description = Editgame.Description;
			game.Name = Editgame.Name;
			game.CategoryId = Editgame.CategoryId;
			game.Games = Editgame.SelectedDevices.Select(x=> new GameDevice { DeviceId=x }).ToList();

			if (hasnewcover)
			{
				game.Cover=await SaveCover(Editgame.Cover!);
			}

			var effectedrows = context.SaveChanges();
			if (effectedrows > 0)
			{
				if (hasnewcover)
				{
					var cover = Path.Combine(imagespath,oldimage!);
					File.Delete(cover);
				}
				return game;
			}
			else
			{
				var cover = Path.Combine(imagespath, oldimage!);
				File.Delete(cover);
				return null;
			}

		}

		public Game? GetById(int id)
		{
			return context.Games
				.Include(x => x.Category)
				.Include(x => x.Games)
				.ThenInclude(x => x.Device)
				.AsNoTracking()
				.SingleOrDefault(x => x.Id == id);
		}

		IEnumerable<Game> IGameService.GetAll()
		{
			return context.Games
				.Include(x=>x.Category)
				.Include(x=>x.Games)
				.ThenInclude(x=>x.Device)
				.AsNoTracking()
				.ToList();
		}
		private async Task<string> SaveCover(IFormFile cover)
		{
			var covername = $"{Guid.NewGuid()} {Path.GetExtension(cover.FileName)}";
			var path = Path.Combine(imagespath, covername);

			using var stream = File.Create(path);
			await cover.CopyToAsync(stream);
			return covername;
		}
	}
}
