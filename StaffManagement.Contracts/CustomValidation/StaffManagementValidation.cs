using System.ComponentModel.DataAnnotations;

namespace StaffManagement;
public class ValidateEmail : ValidationAttribute
{
    /// <summary>
    /// Here we are using built-in validation
    /// Sometimes I use tools like "Fluent validation" (If the project is much more complicated that this demo)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
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
