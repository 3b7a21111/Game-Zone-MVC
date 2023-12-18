using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services.Device_Repository
{
	public class Devices : IDevices
	{
		private readonly ApplicationDbContext context;

		public Devices(ApplicationDbContext context)
        {
			this.context = context;
		}
        IEnumerable<SelectListItem> IDevices.GetListofDevice()
		{
			return context.Devices
				.Select(d=> new SelectListItem { Value=d.Id.ToString(), Text=d.Name })
				.OrderBy(x=>x.Text)
				.AsNoTracking()
				.ToList();
		}
	}
}
