using Revenue_recognition_system.Domain.Constants;
using Revenue_recognition_system.Domain.Entities;
using Revenue_recognition_system.Domain.Exceptions;
using Revenue_recognition_system.Domain.Repositories;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Interfaces;

namespace Revenue_recognition_system.Services.Implementations;

public class ContractService(
    IContractRepository contractRepository,
    IClientRepository clientRepository,
    ISoftwareRepository softwareRepository) : IContractService
{
    public async Task<GetContractDto> GetByIdAsync(int contractId)
    {
        var contract = await contractRepository.GetByIdAsync(contractId);
        
        if (contract == null)
            throw new NotFoundException($"Contract with contractId {contractId} doesn't exist.");
        
        return new GetContractDto
        {
            Id = contract.Id,
            ClientId = contract.ClientId,
            SoftwareVersionId = contract.SoftwareVersionId,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            FinalPrice = contract.FinalPrice,
            AdditionalSupportYears = contract.AdditionalSupportYears,
            SignedAt = contract.SignedAt
        };
    }

    public async Task<GetContractDto> AddAsync(AddContractDto dto)
    {
        if (dto.ClientId <= 0)
            throw new BadRequestException("ClientId must be greater than 0.");
        if (dto.SoftwareVersionId <= 0)
            throw new BadRequestException("SoftwareVersionId must be greater than 0.");

        if (!await clientRepository.IdExistsAsync(dto.ClientId))
            throw new NotFoundException($"Client with id {dto.ClientId} doesn't exist.");

        var softwareVersion = await softwareRepository.GetSoftwareVersionWithSoftwareAsync(dto.SoftwareVersionId);
        if (softwareVersion == null)
            throw new NotFoundException($"SoftwareVersion with id {dto.SoftwareVersionId} doesn't exist.");

        if (dto.AdditionalSupportYears is < ContractConstraints.AdditionalSupportYearsMinValue or > ContractConstraints.AdditionalSupportYearsMaxValue)
            throw new BadRequestException($"AdditionalSupportYears must be between {ContractConstraints.AdditionalSupportYearsMinValue} and {ContractConstraints.AdditionalSupportYearsMaxValue}.");
        
        
        var startDate = DateTime.UtcNow.Date;
        var endDate = dto.EndDate.Date; 

        // Validate end date window [3, 30] days from start
        if (endDate < startDate.AddDays(ContractConstraints.DaysMinValue))
            throw new BadRequestException($"EndDate must be at least {ContractConstraints.DaysMinValue} days after StartDate (today).");
        if (endDate > startDate.AddDays(ContractConstraints.DaysMaxValue))
            throw new BadRequestException($"EndDate cannot be more than {ContractConstraints.DaysMaxValue} days after StartDate (today).");

        // Compute final price using integer day difference
        var days = (endDate - startDate).Days; // non-negative by validation
        var licensePrice = softwareVersion.Software.LicensePrice;

        // Pricing formula (decimal for money precision)
        var basePrice = licensePrice + dto.AdditionalSupportYears * ContractConstraints.AdditionalSupportYearsPricePerYear; 
        var multiplier = 1m + days * ContractConstraints.PricePerDayMultiplier;
        var finalPrice = decimal.Round(basePrice * multiplier, 2, MidpointRounding.ToEven);

        var toAdd = new Contract
        {
            ClientId = dto.ClientId,
            SoftwareVersionId = dto.SoftwareVersionId,
            StartDate = startDate,
            EndDate = endDate,
            FinalPrice = finalPrice,
            AdditionalSupportYears = dto.AdditionalSupportYears,
            SignedAt = null
        };

        await contractRepository.AddAsync(toAdd);
        await contractRepository.SaveChangesAsync();

        // Build response DTO 
        return new GetContractDto
        {
            Id = toAdd.Id,
            ClientId = toAdd.ClientId,
            SoftwareVersionId = toAdd.SoftwareVersionId,
            StartDate = toAdd.StartDate,
            EndDate = toAdd.EndDate,
            FinalPrice = toAdd.FinalPrice,
            AdditionalSupportYears = toAdd.AdditionalSupportYears,
            SignedAt = toAdd.SignedAt
        };
    }
}
