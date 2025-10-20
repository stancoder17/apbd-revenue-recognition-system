using Revenue_recognition_system.Services.DTOs;

namespace Revenue_recognition_system.Services.Interfaces;

public interface IClientService
{
    public Task<GetIndividualClientDto> GetIndividualByIdAsync(int clientId);
    public Task<GetCompanyClientDto> GetCompanyByIdAsync(int clientId);
    public Task<GetIndividualClientDto> AddIndividualAsync(AddIndividualClientDto dto);
    public Task<GetCompanyClientDto> AddCompanyAsync(AddCompanyClientDto dto);
    public Task<GetIndividualClientDto> UpdateIndividualAsync(int clientId, UpdateIndividualClientDto dto);
    public Task<GetCompanyClientDto> UpdateCompanyAsync(int clientId, UpdateCompanyClientDto dto);
    public Task DeleteIndividualAsync(int clientId);
}
