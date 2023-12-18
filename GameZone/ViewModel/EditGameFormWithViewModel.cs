using GameZone.Attribute_Validation;
using GameZone.Settings;

namespace GameZone.ViewModel
{
	public class EditGameFormWithViewModel:GameFormWithViewModel
	{
        public int Id { get; set; }

        public string? CurrentCover { get; set; }
        [AllowedExtensions(FileSettings.AllowedExtension)
			, MaxFileSize(FileSettings.MaxFileSizeinBytes)]
		public IFormFile ? Cover { get; set; } = default!;
	}
}
