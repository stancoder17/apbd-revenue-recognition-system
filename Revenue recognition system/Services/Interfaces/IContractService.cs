using Revenue_recognition_system.Services.DTOs;

namespace Revenue_recognition_system.Services.Interfaces;

public interface IContractService
{
    Task<GetContractDto> GetByIdAsync(int contractId);
    Task<GetContractDto> AddAsync(AddContractDto dto);
}
