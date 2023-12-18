using GameZone.Attribute_Validation;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModel
{
	public class CreateGameFormWithViewModel: GameFormWithViewModel
	{
		
		[AllowedExtensions(FileSettings.AllowedExtension)
			,MaxFileSize(FileSettings.MaxFileSizeinBytes)]
		public IFormFile Cover { get; set; } = default!;
    }
}
