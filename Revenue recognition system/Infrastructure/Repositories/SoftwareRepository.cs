using Microsoft.EntityFrameworkCore;
using Revenue_recognition_system.Data;
using Revenue_recognition_system.Domain.Entities;
using Revenue_recognition_system.Domain.Repositories;

namespace Revenue_recognition_system.Repositories;

public class SoftwareRepository(AppDbContext context) : ISoftwareRepository
{
    public async Task<SoftwareVersion?> GetSoftwareVersionWithSoftwareAsync(int softwareVersionId)
    {
        return await context.SoftwareVersions
            .Include(sv => sv.Software)
            .AsNoTracking()
            .FirstOrDefaultAsync(sv => sv.Id == softwareVersionId);
    }
}