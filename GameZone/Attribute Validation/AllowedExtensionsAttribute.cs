using System.ComponentModel.DataAnnotations;

namespace GameZone.Attribute_Validation
{
	public class AllowedExtensionsAttribute:ValidationAttribute
	{
		private readonly string allowedExtensions;

		public AllowedExtensionsAttribute(string AllowedExtensions)
        {
			allowedExtensions = AllowedExtensions;
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file is not null)
			{
				var extension = Path.GetExtension(file.FileName);
				var isallowed = allowedExtensions.Split(",").Contains(extension, StringComparer.OrdinalIgnoreCase);
				if (!isallowed)
				{
					return new ValidationResult($"Only {allowedExtensions} are allowed");
				}
			}
			return ValidationResult.Success;
		}
	}
}
