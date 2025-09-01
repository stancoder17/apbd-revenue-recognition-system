using Revenue_recognition_system.Application.DTOs;
using Revenue_recognition_system.Domain.Entities;
using Revenue_recognition_system.Domain.Repositories.Interfaces;
using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Application.Services;

public class ClientService(IClientRepository clientRepository)
{
    public async Task<int> AddIndividualClientAsync(CreateIndividualClientDto dto)
        {
            // validate required fields
            if (string.IsNullOrWhiteSpace(dto.FirstName)) throw new ArgumentException("FirstName is required");
            if (string.IsNullOrWhiteSpace(dto.LastName)) throw new ArgumentException("LastName is required");
            if (string.IsNullOrWhiteSpace(dto.Pesel) || dto.Pesel.Length != 11) throw new ArgumentException("Pesel must be 11 chars");
            if (string.IsNullOrWhiteSpace(dto.Email)) throw new ArgumentException("Email is required");
            if (string.IsNullOrWhiteSpace(dto.PhoneNumber)) throw new ArgumentException("PhoneNumber is required");

            // address must exist
            if (!await clientRepository.AddressExistsAsync(dto.AddressId))
                throw new InvalidOperationException($"Address with id {dto.AddressId} not found");

            // pesel uniqueness
            if (await clientRepository.IndividualPeselExistsAsync(dto.Pesel))
                throw new InvalidOperationException($"Individual with PESEL {dto.Pesel} already exists");

            var individual = new IndividualClient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Pesel = dto.Pesel,
                IsDeleted = false,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                AddressId = dto.AddressId
            };

            await clientRepository.AddAsync(individual);
            await clientRepository.SaveChangesAsync();

            return individual.Id;
        }

        public async Task<int> AddCompanyClientAsync(CreateCompanyClientDto dto)
        {
            // validate required fields
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(dto.Krs) || dto.Krs.Length != 10) throw new ArgumentException("KRS must be 10 chars");
            if (string.IsNullOrWhiteSpace(dto.Email)) throw new ArgumentException("Email is required");
            if (string.IsNullOrWhiteSpace(dto.PhoneNumber)) throw new ArgumentException("PhoneNumber is required");

            // address must exist
            if (!await clientRepository.AddressExistsAsync(dto.AddressId))
                throw new InvalidOperationException($"Address with id {dto.AddressId} not found");

            // krs uniqueness
            if (await clientRepository.CompanyKrsExistsAsync(dto.Krs))
                throw new InvalidOperationException($"Company with KRS {dto.Krs} already exists");

            var company = new CompanyClient
            {
                Name = dto.Name,
                Krs = dto.Krs,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                AddressId = dto.AddressId
            };

            await clientRepository.AddAsync(company);
            await clientRepository.SaveChangesAsync();

            return company.Id;
        }
}