using Revenue_recognition_system.Services.DTOs;

namespace Revenue_recognition_system.Services.Interfaces;

public interface IClientService
{
    public Task<GetIndividualClientDto> GetIndividualClientByIdAsync(int clientId);
    public Task<GetCompanyClientDto> GetCompanyClientByIdAsync(int clientId);
    public Task<GetIndividualClientDto> AddIndividualClientAsync(AddIndividualClientDto dto);
    public Task<GetCompanyClientDto> AddCompanyClientAsync(AddCompanyClientDto dto);
    
}
