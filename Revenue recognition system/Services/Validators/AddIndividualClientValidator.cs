using FluentValidation;
using Revenue_recognition_system.Domain.Constants;
using Revenue_recognition_system.Services.DTOs;

namespace Revenue_recognition_system.Services.Validators;

public class AddIndividualClientValidator : AbstractValidator<AddIndividualClientDto>
{
    public AddIndividualClientValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(ClientConstraints.FirstNameMaxLength).WithMessage($"FirstName cannot be longer than {ClientConstraints.FirstNameMaxLength} characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(ClientConstraints.LastNameMaxLength).WithMessage($"LastName cannot be longer than {ClientConstraints.LastNameMaxLength} characters.");

        RuleFor(x => x.Pesel)
            .NotEmpty().WithMessage("PESEL is required.")
            .Length(ClientConstraints.PeselLength).WithMessage($"PESEL must be {ClientConstraints.PeselLength} characters long.")
            .Matches(@"^\d+$").WithMessage("PESEL must contain only digits.");

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
