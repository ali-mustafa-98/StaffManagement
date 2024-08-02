using CWS.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.Timing;
using System.Linq.Expressions;

namespace CWS.CustomValidation;
/// <summary>
/// Not Empty Guid
/// </summary>
public class NotEmptyGuidAttribute : ValidationAttribute, ITransientDependency
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var localizer = validationContext.GetRequiredService<IStringLocalizer<CWSResource>>();

        ErrorMessage = localizer[CWSDomainErrorCodes.DataIsRequired];

        var result = new ValidationResult(ErrorMessage);

        if (value is null)
        {
            return null;
        }
        if (value is Guid guidValue)
        {
            if (guidValue != Guid.Empty)
            {
                return null;
            }
        }
        return result;
    }
}
/// <summary>
/// Date is required and not in the past
/// </summary>
public class RequiredDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var _clock = validationContext.GetRequiredService<IClock>();
        var localizer = validationContext.GetRequiredService<IStringLocalizer<CWSResource>>();
        ValidationResult? result = null;
        var minDate = _clock.Normalize(DateTime.MinValue);
        if (value is DateTime date)
        {
            if (_clock.Normalize(date) == minDate)
            {
                result = new ValidationResult(localizer[CWSDomainErrorCodes.DataIsRequired]);
            }
            else if (_clock.Normalize(date).Date < _clock.Now.Date)
            {
                result = new ValidationResult(localizer[CWSDomainErrorCodes.InvalidDate]);
            }
        }
        return result;
    }
}
/// <summary>
/// Date is not in the past
/// </summary>
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var _clock = validationContext.GetRequiredService<IClock>();
        var localizer = validationContext.GetRequiredService<IStringLocalizer<CWSResource>>();
        ErrorMessage = localizer[CWSDomainErrorCodes.CannotEnterDateFromThePast];
        ValidationResult? result = new(ErrorMessage);
        if (value is DateTime date)
        {
            if (_clock.Normalize(date).Date < _clock.Now.Date)
            {
                return result;
            }
        }
        return null;
    }
}
/// <summary>
/// Value must be non-negative (and bigger than zero if passed false).
/// </summary>
public class NotNegativeRangeAttribute(bool withZero = true) : RangeAttribute(withZero ? 0 : 1, int.MaxValue)
{
    private readonly int minimum = withZero ? 0 : 1;
    private readonly string errorCode = withZero ? CWSDomainErrorCodes.NotNegative : CWSDomainErrorCodes.BiggerThanZero;

    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var localizer = validationContext.GetRequiredService<IStringLocalizer<CWSResource>>();

        ErrorMessage = localizer[errorCode];

        var result = new ValidationResult(ErrorMessage);

        if (value is null)
        {
            return null;
        }
        if (value is int intValue)
        {
            if (intValue >= minimum && intValue < int.MaxValue)
            {
                return null;
            }
        }
        else if (value is double doubleValue)
        {
            if (doubleValue >= Convert.ToDouble(minimum) && doubleValue < double.MaxValue)
            {
                return null;
            }
        }
        return result;
    }

}




public class EnumValidationAttribute<TEnum> : ValidationAttribute where TEnum : Enum
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var localizer = validationContext.GetRequiredService<IStringLocalizer<CWSResource>>();
        ErrorMessage = localizer[CWSDomainErrorCodes.InvalidEnumType];
        var result = new ValidationResult(ErrorMessage);

        if (value is TEnum enumValue && Enum.IsDefined(typeof(TEnum), enumValue))
        {
            return null;
        }

        return result;
    }
}

/// <summary>
/// This Method to validate if Min and Max are both null
/// </summary>
/// <typeparam name="T"></typeparam>
public class MinMaxNullValidationAttribute(string minValue) : ValidationAttribute
{
    private readonly string _minValue = minValue;

    protected override ValidationResult? IsValid(object maxValue, ValidationContext validationContext)
    {
        var localizer = validationContext.GetRequiredService<IStringLocalizer<CWSResource>>();

        var property = validationContext.ObjectType.GetProperty(_minValue);

        var minValue = property?.GetValue(validationContext.ObjectInstance);

        if (minValue is null && maxValue is null)
        {
            ErrorMessage = localizer[CWSDomainErrorCodes.EitherMaximumValueOrMinimumValueMustBeEntered];
            return new ValidationResult(ErrorMessage);
        }
        else
        {
            return null;
        }
    }
}

/// <summary>
/// This method to validate that min must be smaller than max
/// </summary>
/// <typeparam name="T"></typeparam>
public class MinSmallerThanMaxValidationAttribute<T>(string minValue) : ValidationAttribute where T : IComparable<T>
{
    private readonly string _minValue = minValue;

    protected override ValidationResult? IsValid(object maxvalue, ValidationContext validationContext)
    {
        var localizer = validationContext.GetRequiredService<IStringLocalizer<CWSResource>>();

        var property = validationContext.ObjectType.GetProperty(_minValue);

        var minValue = property?.GetValue(validationContext.ObjectInstance);

        if (minValue is not null && maxvalue is not null)
        {
            if (((IComparable<T>)minValue).CompareTo((T)maxvalue) > 0)
            {
                ErrorMessage = localizer[CWSDomainErrorCodes.MinimumValueSmallerThanMaximumValue];
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}

public class UniqueNamesValidationAttribute<T>(string entityName, string propertyName1, string? propertyName2 = null) : ValidationAttribute where T : class
{
    private readonly string propertyName1 = propertyName1;
    private readonly string? propertyName2 = propertyName2;
    private readonly string entityName = entityName;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var localizer = validationContext.GetRequiredService<IStringLocalizer<CWSResource>>();
        ErrorMessage = string.Format(localizer[CWSDomainErrorCodes.EntityMustHaveUniqueName], entityName);

        if (value is List<T> entities)
        {

            var lambda = CreateLambdaExpression(propertyName1, entities);
            var names = entities.Select(lambda.Compile()).Where(x => x != null);

            if (names.Count() > names.Distinct().Count())
                return new ValidationResult(ErrorMessage);

            if (!string.IsNullOrEmpty(propertyName2))
            {
                lambda = CreateLambdaExpression(propertyName2, entities);
                names = entities.Select(lambda.Compile());
                names = names.Where(x => x != null);
                if (names.Count() > names.Distinct().Count())
                    return new ValidationResult(ErrorMessage);
            }
        }
        return null;
    }

    private Expression<Func<T, string>> CreateLambdaExpression(string property, List<T> entities)
    {

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyExpression = Expression.PropertyOrField(parameter, property);

        var nullCheck = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));
        var conditionalToLower = Expression.Condition(nullCheck,
                                    Expression.Call(propertyExpression, typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)),
                                    Expression.Constant(null, typeof(string)));

        var lambda = Expression.Lambda<Func<T, string>>(conditionalToLower, parameter);
        return lambda;
    }
}
#region almarfaa
public class IsAddressRequiredAttribute(string withTransport) : ValidationAttribute
{
    private readonly string _withTransport = withTransport;
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var localizer = validationContext.GetRequiredService<IStringLocalizer<CWSResource>>();
        ErrorMessage = localizer[CWSDomainErrorCodes.AddressIsRequired];

        var result = new ValidationResult(ErrorMessage);

        var property = validationContext.ObjectType.GetProperty(_withTransport);

        var withTransport = property?.GetValue(validationContext.ObjectInstance);

        if (withTransport is bool withTransportValue)
        {
            if (withTransportValue && value is null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        return null;
    }
}
#endregion