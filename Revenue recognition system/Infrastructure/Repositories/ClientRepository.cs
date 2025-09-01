using Microsoft.EntityFrameworkCore;
using Revenue_recognition_system.Data;
using Revenue_recognition_system.Domain.Repositories.Interfaces;
using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Infrastructure.Repositories;

public class ClientRepository(AppDbContext context) : IClientRepository
{
    public async Task AddAsync(Client client)
    {
        await context.AddAsync(client);
    }

    public async Task<bool> IndividualPeselExistsAsync(string pesel)
    {
        return await context.IndividualClients
            .AsNoTracking()
            .AnyAsync(ic => ic.Pesel == pesel);
    }

    public async Task<bool> CompanyKrsExistsAsync(string krs)
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