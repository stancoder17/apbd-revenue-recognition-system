using Revenue_recognition_system.Domain.Entities;
using Revenue_recognition_system.Domain.Exceptions;
using Revenue_recognition_system.Domain.Repositories;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Interfaces;

namespace Revenue_recognition_system.Services.Implementations;

public class ClientService(IClientRepository repository) : IClientService
{
    public async Task<GetIndividualClientDto> GetIndividualClientByIdAsync(int clientId)
    {
        var client = await repository.GetIndividualByIdAsync(clientId);
        
        if (client == null)
            throw new NotFoundException($"Individual client with id {clientId} doesn't exist.");

        return new GetIndividualClientDto
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Pesel = client.Pesel,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            AddressId = client.AddressId
        };
    }
    
    public async Task<GetCompanyClientDto> GetCompanyClientByIdAsync(int clientId)
    {
        var client = await repository.GetCompanyByIdAsync(clientId);
        
        if (client == null)
            throw new NotFoundException($"Company client with id {clientId} doesn't exist.");

        return new GetCompanyClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Krs = client.Krs,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            AddressId = client.AddressId
        };
    }

    public async Task<GetIndividualClientDto> AddIndividualClientAsync(AddIndividualClientDto dto)
    {
        if (await repository.PeselExistsAsync(dto.Pesel))
            throw new AlreadyExistsException($"Individual client with PESEL {dto.Pesel} already exists.");

        if (!await repository.AddressExistsAsync(dto.AddressId))
            throw new NotFoundException("Address doesn't exist.");

        var newClient = new IndividualClient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Pesel = dto.Pesel,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            AddressId = dto.AddressId,
            IsDeleted = false
        };

        await repository.AddAsync(newClient);
        await repository.SaveChangesAsync();

        return new GetIndividualClientDto
        {
            Id = newClient.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Pesel = dto.Pesel,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            AddressId = dto.AddressId
        };
    }

    public async Task<GetCompanyClientDto> AddCompanyClientAsync(AddCompanyClientDto dto)
    {
        if (await repository.KrsExistsAsync(dto.Krs))
            throw new AlreadyExistsException($"Company with KRS {dto.Krs} already exists.");

        if (!await repository.AddressExistsAsync(dto.AddressId))
            throw new NotFoundException("Address does not exist.");

        var newClient = new CompanyClient
        {
            Name = dto.Name,
            Krs = dto.Krs,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            AddressId = dto.AddressId
        };

        await repository.AddAsync(newClient);
        await repository.SaveChangesAsync();

        return new GetCompanyClientDto
        {
            Name = newClient.Name,
            Krs = newClient.Krs,
            Email = newClient.Email,
            PhoneNumber = newClient.PhoneNumber,
            AddressId = newClient.AddressId
        };
    }
}
