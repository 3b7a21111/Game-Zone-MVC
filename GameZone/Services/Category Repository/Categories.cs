using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services.Category_Repository
{
	public class Categories : ICategories
	{
		private readonly ApplicationDbContext context;

		public Categories(ApplicationDbContext context)
        {
			this.context = context;
		}
        public IEnumerable<SelectListItem> GetListOfGategory()
		{
			return context.Categories
				.Select(c=> new SelectListItem { Value=c.Id.ToString(),Text=c.Name})
				.OrderBy(x=>x.Text)
				.AsNoTracking()
				.ToList();
		}
	}
}
