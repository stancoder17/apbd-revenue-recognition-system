using Revenue_recognition_system.Domain.Entities;

namespace Revenue_recognition_system.Domain.Repositories;

public interface IContractRepository
{
    Task<Contract?> GetByIdAsync(int contractId);
}
