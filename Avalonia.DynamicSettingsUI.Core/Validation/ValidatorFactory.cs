using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Validation.Validators;

namespace Avalonia.DynamicSettingsUI.Core.Validation;

public class ValidatorFactory
{
    private readonly Dictionary<ValidationType, Func<IValidator>> _validators = [];

    public ValidatorFactory()
    {
        RegisterDefaultValidators();
    }

    private void RegisterDefaultValidators()
    {
        Register(ValidationType.AlphaNumeric, () => new AlphaNumericValidator());
        Register(ValidationType.AlphaOnly, () => new AlphaOnlyValidator());
        Register(ValidationType.DateRange, () => new DateRangeValidator());
        Register(ValidationType.DirectoryExists, () => new DirectoryExistsValidator());
        Register(ValidationType.ExactLength, () => new ExactLengthValidator());
        Register(ValidationType.Email, () => new EmailValidator());
        Register(ValidationType.Email, () => new EmailValidator());
        Register(ValidationType.FileExists, () => new FileExistsValidator());
        Register(ValidationType.FileExtension, () => new FileExtensionValidator());
        Register(ValidationType.FutureDate, () => new FutureDateValidator());
        Register(ValidationType.GreaterThan, () => new GreaterThanValidator());
        Register(ValidationType.LessThan, () => new LessThanValidator());
        Register(ValidationType.MaxFileSize, () => new MaxFileSizeValidator());
        Register(ValidationType.MaxLength, () => new MaxLengthValidator());
        Register(ValidationType.MinLength, () => new MinLengthValidator());
        Register(ValidationType.NoSpecialChars, () => new NoSpecialCharsValidator());
        Register(ValidationType.NotEmpty, () => new NotEmptyValidator());
        Register(ValidationType.NotNull, () => new NotNullValidator());
        Register(ValidationType.NotWhiteSpace, () => new NotWhiteSpaceValidator());
        Register(ValidationType.PastDate, () => new PastDateValidator());
        Register(ValidationType.Positive, () => new PositiveValidator());
        Register(ValidationType.Range, () => new RangeValidator());
        Register(ValidationType.Regex, () => new RegexValidator());
        Register(ValidationType.Url, () => new UrlValidator());
    }

    public void Register(ValidationType type, Func<IValidator> factory)
    {
        _validators[type] = factory;
    }

    public IValidator? Create(ValidationType type)
    {
        return _validators.TryGetValue(type, out Func<IValidator>? validator) ? validator.Invoke() : null;
    }
}
