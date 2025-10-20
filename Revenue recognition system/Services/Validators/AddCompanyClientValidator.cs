using FluentValidation;
using Revenue_recognition_system.Domain.Constants;
using Revenue_recognition_system.Services.DTOs;

namespace Revenue_recognition_system.Services.Validators;

public class AddCompanyClientValidator : AbstractValidator<AddCompanyClientDto>
{
    public AddCompanyClientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Company name is required.")
            .MaximumLength(ClientConstraints.CompanyNameMaxLength).WithMessage($"Company name cannot be longer than {ClientConstraints.CompanyNameMaxLength} characters.");

        RuleFor(x => x.Krs)
            .NotEmpty().WithMessage("KRS is required.")
            .Length(ClientConstraints.KrsLength).WithMessage($"KRS must be {ClientConstraints.KrsLength} characters long.")
            .Matches(@"^\d+$").WithMessage("KRS must contain only digits.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(ClientConstraints.EmailMaxLength).WithMessage($"Email cannot be longer than {ClientConstraints.EmailMaxLength} characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(ClientConstraints.PhoneNumberMaxLength).WithMessage($"PhoneNumber cannot be longer than {ClientConstraints.PhoneNumberMaxLength} characters.");

        RuleFor(x => x.AddressId)
            .GreaterThan(0).WithMessage("AddressId is invalid.");
    }
}
