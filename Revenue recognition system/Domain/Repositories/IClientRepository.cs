using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Domain.Repositories.Interfaces;

public interface IClientRepository
{
    public Task AddAsync(Client client);
    public Task<bool> IndividualPeselExistsAsync(string pesel);
    public Task<bool> CompanyKrsExistsAsync(string krs);
    public Task<bool> AddressExistsAsync(int addressId);
    public Task SaveChangesAsync();
}