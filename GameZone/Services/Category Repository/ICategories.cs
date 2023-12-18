using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
	public interface ICategories
	{
		IEnumerable<SelectListItem> GetListOfGategory();
	}
}
