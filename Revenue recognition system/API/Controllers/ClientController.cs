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
            return Ok(await service.GetIndividualByIdAsync(clientId));
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
            var client = await service.AddIndividualAsync(dto);
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

    [HttpDelete("individual/{clientId:int}")]
    public async Task<IActionResult> DeleteIndividual(int clientId)
    {
        try
        {
            await service.DeleteIndividualAsync(clientId);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("company/{clientId:int}")]
    public async Task<IActionResult> GetCompanyById(int clientId)
    {
        try
        {
            return Ok(await service.GetCompanyByIdAsync(clientId));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("company")]
    public async Task<IActionResult> AddCompany([FromBody] AddCompanyClientDto dto)
    {
        try
        {
            var client = await service.AddCompanyAsync(dto);
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
