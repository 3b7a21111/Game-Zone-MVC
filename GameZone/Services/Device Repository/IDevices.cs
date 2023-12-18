using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services.Device_Repository
{
	public interface IDevices
	{
		IEnumerable<SelectListItem> GetListofDevice(); 
	}
}
