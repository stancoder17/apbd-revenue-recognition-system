using Revenue_recognition_system.Domain.Entities;

namespace Revenue_recognition_system.Domain.Repositories;

public interface ISoftwareRepository
{
    Task<SoftwareVersion?> GetSoftwareVersionWithSoftwareAsync(int softwareVersionId);
}