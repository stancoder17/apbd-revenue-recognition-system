using Microsoft.EntityFrameworkCore;
using Revenue_recognition_system.Data;
using Revenue_recognition_system.Domain.Entities;
using Revenue_recognition_system.Domain.Repositories;

namespace Revenue_recognition_system.Infrastructure.Repositories;

public class ContractRepository(AppDbContext context) : IContractRepository
{
    public async Task<Contract?> GetByIdAsync(int contractId)
    {
        return await context.Contracts
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == contractId);
    }
}
