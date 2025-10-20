using Revenue_recognition_system.Domain.Entities;

namespace Revenue_recognition_system.Domain.Repositories;

public interface IClientRepository
{
    public Task<Client?> GetByIdAsync(int clientId);
    public Task<IndividualClient?> GetIndividualByIdAsync(int clientId);
    public Task<CompanyClient?> GetCompanyByIdAsync(int clientId);
    public Task AddAsync(Client client);
    public Task<int> UpdateIndividualAsync(int clientId, string firstName, string lastName, string email, string phoneNumber, int addressId);
    public Task<int> UpdateCompanyAsync(int clientId, string name, string email, string phoneNumber, int addressId);
    public Task<bool> IdExistsAsync(int clientId);
    public Task<bool> PeselExistsAsync(string pesel);
    public Task<bool> KrsExistsAsync(string krs);
    public Task<bool> AddressExistsAsync(int addressId);
    public Task SaveChangesAsync();
}