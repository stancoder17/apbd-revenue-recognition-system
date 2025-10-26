using FluentValidation;
using Revenue_recognition_system.Domain.Constants;
using Revenue_recognition_system.Services.DTOs;

namespace Revenue_recognition_system.Services.Validators;

public class AddContractValidator : AbstractValidator<AddContractDto>
{
    public AddContractValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("ClientId is required.")
            .GreaterThan(0).WithMessage("ClientId must be greater than 0.");

        RuleFor(x => x.SoftwareVersionId)
            .NotEmpty().WithMessage("SoftwareVersionId is required.")
            .GreaterThan(0).WithMessage("SoftwareVersionId must be greater than 0.");

        RuleFor(x => x.AdditionalSupportYears)
            .InclusiveBetween(
                ContractConstraints.AdditionalSupportYearsMinValue,
                ContractConstraints.AdditionalSupportYearsMaxValue
            )
            .WithMessage($"AdditionalSupportYears must be between {ContractConstraints.AdditionalSupportYearsMinValue} and {ContractConstraints.AdditionalSupportYearsMaxValue}.");


        // EndDate range depends on current date; leave the 3..30 days window validation to the service,
        // because it depends on 'now' at execution time.
        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.");
    }
}