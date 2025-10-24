using Microsoft.EntityFrameworkCore;
using Revenue_recognition_system.Data;
using Revenue_recognition_system.Domain.Entities;
using Revenue_recognition_system.Domain.Repositories;

namespace Revenue_recognition_system.Infrastructure.Repositories;

public class ClientRepository(AppDbContext context) : IClientRepository
{
    public async Task<Client?> GetByIdAsync(int clientId)
    {
        return await context.Clients
            .Include(c => (c as IndividualClient)!.Address)
            .Include(c => (c as CompanyClient)!.Address)
            .FirstOrDefaultAsync(c => c.Id == clientId);
    }
    
    public async Task<IndividualClient?> GetIndividualByIdAsync(int clientId)
    {
        return await context.IndividualClients
            .Include(ic => ic.Address)
            .FirstOrDefaultAsync(c => c.Id == clientId);
    }
    
    public async Task<CompanyClient?> GetCompanyByIdAsync(int clientId)
    {
        return await context.CompanyClients
            .Include(cc => cc.Address)
            .FirstOrDefaultAsync(c => c.Id == clientId);
    }
    
    public async Task AddAsync(Client client)
    {
        await context.Clients.AddAsync(client);
    }

    public async Task<int> UpdateIndividualAsync(int clientId, string firstName, string lastName, string email, string phoneNumber, int addressId)
    {
        // Update selected columns for IndividualClient by id
        return await context.IndividualClients
            .Where(e => e.Id == clientId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(e => e.FirstName, firstName)
                .SetProperty(e => e.LastName, lastName)
                .SetProperty(e => e.Email, email)
                .SetProperty(e => e.PhoneNumber, phoneNumber)
                .SetProperty(e => e.AddressId, addressId)
            );
    }

    public async Task<int> UpdateCompanyAsync(int clientId, string name, string email, string phoneNumber, int addressId)
    {
        // Update selected columns for CompanyClient by id
        return await context.CompanyClients
            .Where(e => e.Id == clientId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(e => e.Name, name)
                .SetProperty(e => e.Email, email)
                .SetProperty(e => e.PhoneNumber, phoneNumber)
                .SetProperty(e => e.AddressId, addressId)
            );
    }

    public async Task<bool> IdExistsAsync(int clientId)
    {
        return await context.Clients
            .AsNoTracking()
            .AnyAsync(x => x.Id == clientId);
    }

    public async Task<bool> PeselExistsAsync(string pesel)
    {
        return await context.IndividualClients
            .AsNoTracking()
            .AnyAsync(ic => ic.Pesel == pesel);
    }

    public async Task<bool> KrsExistsAsync(string krs)
    {
        return await context.CompanyClients
            .AsNoTracking()
            .AnyAsync(cc => cc.Krs == krs);
    }

    public async Task<bool> AddressExistsAsync(int addressId)
    {
        return await context.Addresses
            .AsNoTracking()
            .AnyAsync(a => a.Id == addressId);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}