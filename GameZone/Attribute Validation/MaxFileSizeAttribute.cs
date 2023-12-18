using System.ComponentModel.DataAnnotations;

namespace GameZone.Attribute_Validation
{
	public class MaxFileSizeAttribute:ValidationAttribute
	{
		private readonly int maxFileSize;
		public MaxFileSizeAttribute(int MaxFileSize)
        {
			maxFileSize = MaxFileSize;
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if(file is not null) 
			{
				if (file.Length > maxFileSize)
				{
					return new ValidationResult($"Maximum allowed size is {maxFileSize} Bytes");
				}
			}
			return ValidationResult.Success;
		}
	}
}
