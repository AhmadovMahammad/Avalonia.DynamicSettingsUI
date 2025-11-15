using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Validation.Validators;

namespace Avalonia.DynamicSettingsUI.Core.Validation;

public class ValidatorFactoryRegistry
{
    private readonly Dictionary<ValidationType, IValidator> _validators = [];

    public ValidatorFactoryRegistry()
    {
        RegisterDefaultValidators();
    }

    private void RegisterDefaultValidators()
    {
        Register(new AlphaNumericValidator());
        Register(new AlphaOnlyValidator());
        Register(new DateRangeValidator());
        Register(new DirectoryExistsValidator());
        Register(new ExactLengthValidator());
        Register(new EmailValidator());
        Register(new FileExistsValidator());
        Register(new FileExtensionValidator());
        Register(new FutureDateValidator());
        Register(new GreaterThanValidator());
        Register(new LessThanValidator());
        Register(new MaxFileSizeValidator());
        Register(new MaxLengthValidator());
        Register(new MinLengthValidator());
        Register(new NoSpecialCharsValidator());
        Register(new NotEmptyValidator());
        Register(new NotNullValidator());
        Register(new NotWhiteSpaceValidator());
        Register(new PastDateValidator());
        Register(new PositiveValidator());
        Register(new RangeValidator());
        Register(new RegexValidator());
        Register(new UrlValidator());
    }

    public void Register(IValidator validator)
    {
        _validators[validator.ValidationType] = validator;
    }

    public IValidator? GetValidator(ValidationType type)
    {
        return _validators.TryGetValue(type, out IValidator? validator) ? validator : null;
    }
}
