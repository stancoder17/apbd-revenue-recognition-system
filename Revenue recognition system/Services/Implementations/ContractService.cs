using Revenue_recognition_system.Domain.Exceptions;
using Revenue_recognition_system.Domain.Repositories;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Interfaces;

namespace Revenue_recognition_system.Services.Implementations;

public class ContractService(IContractRepository repository) : IContractService
{
    public async Task<GetContractDto?> GetByIdAsync(int contractId)
    {
        var contract = await repository.GetByIdAsync(contractId);
        
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
}
