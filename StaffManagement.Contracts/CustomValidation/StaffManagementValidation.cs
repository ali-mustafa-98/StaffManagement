using System.ComponentModel.DataAnnotations;

namespace StaffManagement;
public class ValidateEmail : ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		//return base.IsValid(value, validationContext);	

		//if(value is not email)
		//{
		//	throw Exception()
		//}
		return null;
	}
}
