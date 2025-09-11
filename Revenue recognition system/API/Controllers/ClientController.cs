using Microsoft.AspNetCore.Mvc;
using Revenue_recognition_system.Domain.Exceptions;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Interfaces;

namespace Revenue_recognition_system.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService service) : ControllerBase
{
    [HttpGet("individual/{clientId:int}")]
    public async Task<IActionResult> GetIndividualById(int clientId)
    {
        try
        {
            return Ok(await service.GetIndividualClientByIdAsync(clientId));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("company/{clientId:int}")]
    public async Task<IActionResult> GetCompanyById(int clientId)
    {
        try
        {
            return Ok(await service.GetCompanyClientByIdAsync(clientId));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost("individual")]
    public async Task<IActionResult> AddIndividual([FromBody] AddIndividualClientDto dto)
    {
        try
        {
            var client = await service.AddIndividualClientAsync(dto);
            return CreatedAtAction(nameof(GetIndividualById), new { clientId = client.Id }, client);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (AlreadyExistsException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("company")]
    public async Task<IActionResult> AddCompany([FromBody] AddCompanyClientDto dto)
    {
        try
        {
            var client = await service.AddCompanyClientAsync(dto);
            return CreatedAtAction(nameof(GetCompanyById), new { clientId = client.Id }, client);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (AlreadyExistsException e)
        {
            return BadRequest(e.Message);
        }
    }
}
