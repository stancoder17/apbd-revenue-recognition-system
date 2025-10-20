using Revenue_recognition_system.Domain.Entities;
using Revenue_recognition_system.Domain.Exceptions;
using Revenue_recognition_system.Domain.Repositories;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Interfaces;

namespace Revenue_recognition_system.Services.Implementations;

public class ClientService(IClientRepository repository) : IClientService
{
    public async Task<GetIndividualClientDto> GetIndividualByIdAsync(int clientId)
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
            Address = new GetAddressDto
            {
                Id = client.AddressId,
                City = client.Address.City,
                Number = client.Address.Number,
                Street = client.Address.Street,
                PostalCode = client.Address.PostalCode
            }
        };
    }
    
    public async Task<GetCompanyClientDto> GetCompanyByIdAsync(int clientId)    
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
            Address = new GetAddressDto
            {
                Id = client.AddressId,
                City = client.Address.City,
                Street = client.Address.Street,
                Number = client.Address.Number,
                PostalCode = client.Address.PostalCode
            }
        };
    }

    public async Task<GetIndividualClientDto> AddIndividualAsync(AddIndividualClientDto dto)
    {
        if (await repository.PeselExistsAsync(dto.Pesel))
            throw new AlreadyExistsException($"Individual client with PESEL {dto.Pesel} already exists.");

        if (!await repository.AddressExistsAsync(dto.AddressId))
            throw new NotFoundException("Address doesn't exist.");

        var clientToAdd = new IndividualClient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Pesel = dto.Pesel,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            AddressId = dto.AddressId,
            IsDeleted = false
        };

        await repository.AddAsync(clientToAdd);
        await repository.SaveChangesAsync();

        // Get DTO for response
        var createdClient = await repository.GetIndividualByIdAsync(clientToAdd.Id);
        
        return new GetIndividualClientDto
        {
            Id = createdClient!.Id,
            FirstName = createdClient.FirstName,
            LastName = createdClient.LastName,
            Pesel = createdClient.Pesel,
            Email = createdClient.Email,
            PhoneNumber = createdClient.PhoneNumber,
            Address = new GetAddressDto
            {
                Id = createdClient.AddressId,
                City = createdClient.Address.City,
                Street = createdClient.Address.Street,
                Number = createdClient.Address.Number,
                PostalCode = createdClient.Address.PostalCode
            }
        };
    }

    public async Task<GetCompanyClientDto> AddCompanyAsync(AddCompanyClientDto dto)
    {
        if (await repository.KrsExistsAsync(dto.Krs))
            throw new AlreadyExistsException($"Company with KRS {dto.Krs} already exists.");

        if (!await repository.AddressExistsAsync(dto.AddressId))
            throw new NotFoundException("Address doesn't exist.");

        var clientToAdd = new CompanyClient
        {
            Name = dto.Name,
            Krs = dto.Krs,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            AddressId = dto.AddressId
        };

        await repository.AddAsync(clientToAdd);
        await repository.SaveChangesAsync();
        
        // Get DTO for response
        var createdClient = await repository.GetCompanyByIdAsync(clientToAdd.Id);

        return new GetCompanyClientDto
        {
            Name = createdClient!.Name,
            Krs = createdClient.Krs,
            Email = createdClient.Email,
            PhoneNumber = createdClient.PhoneNumber,
            Address = new GetAddressDto
            {
                Id = createdClient.AddressId,
                City = createdClient.Address.City,
                Street = createdClient.Address.Street,
                Number = createdClient.Address.Number,
                PostalCode = createdClient.Address.PostalCode
            }
        };
    }

    public async Task<GetIndividualClientDto> UpdateIndividualAsync(int clientId, UpdateIndividualClientDto dto)
    {
        // Validate address existence
        if (!await repository.AddressExistsAsync(dto.AddressId))
            throw new NotFoundException("Address doesn't exist.");

        // Update using EF bulk update and check affected rows
        var affectedRows = await repository.UpdateIndividualAsync(
            clientId,
            dto.FirstName,
            dto.LastName,
            dto.Email,
            dto.PhoneNumber,
            dto.AddressId);

        if (affectedRows == 0)
            throw new NotFoundException($"Individual client with id {clientId} doesn't exist.");

        // Reload updated entity to build response DTO
        var updated = await repository.GetIndividualByIdAsync(clientId);

        return new GetIndividualClientDto
        {
            Id = updated!.Id,
            FirstName = updated.FirstName,
            LastName = updated.LastName,
            Pesel = updated.Pesel, // immutable
            Email = updated.Email,
            PhoneNumber = updated.PhoneNumber,
            Address = new GetAddressDto
            {
                Id = updated.AddressId,
                City = updated.Address.City,
                Street = updated.Address.Street,
                Number = updated.Address.Number,
                PostalCode = updated.Address.PostalCode
            }
        };
    }

    public async Task<GetCompanyClientDto> UpdateCompanyAsync(int clientId, UpdateCompanyClientDto dto)
    {
        // Validate address existence
        if (!await repository.AddressExistsAsync(dto.AddressId))
            throw new NotFoundException("Address doesn't exist.");

        // Update using EF bulk update and check affected rows
        var affectedRows = await repository.UpdateCompanyAsync(
            clientId,
            dto.Name,
            dto.Email,
            dto.PhoneNumber,
            dto.AddressId);

        if (affectedRows == 0)
            throw new NotFoundException($"Company client with id {clientId} doesn't exist.");

        // Reload updated entity to build response DTO
        var updated = await repository.GetCompanyByIdAsync(clientId);

        return new GetCompanyClientDto
        {
            Id = updated!.Id,
            Name = updated.Name,
            Krs = updated.Krs, // immutable
            Email = updated.Email,
            PhoneNumber = updated.PhoneNumber,
            Address = new GetAddressDto
            {
                Id = updated.AddressId,
                City = updated.Address.City,
                Street = updated.Address.Street,
                Number = updated.Address.Number,
                PostalCode = updated.Address.PostalCode
            }
        };
    }

    public async Task DeleteIndividualAsync(int clientId)
    {
        // If doesn't exist
        var client = await repository.GetByIdAsync(clientId);
        if (client == null)
            throw new NotFoundException("Client doesn't exist.");

        // If not Individual
        if (client is not IndividualClient individual)
            throw new BadRequestException("Client is not an individual client.");

        // If already deleted
        if (individual.IsDeleted)
            throw new BadRequestException("Client is already deleted.");

        // All good -> delete
        individual.IsDeleted = true;
        await repository.SaveChangesAsync();
    }
}
