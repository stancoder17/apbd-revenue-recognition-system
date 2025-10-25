using Microsoft.AspNetCore.Mvc;
using Revenue_recognition_system.Domain.Exceptions;
using Revenue_recognition_system.Services.Interfaces;

namespace Revenue_recognition_system.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractController(IContractService service) : ControllerBase
{
    [HttpGet("{contractId:int}")]
    public async Task<IActionResult> GetById(int contractId)
    {
        try
        {
            return Ok(await service.GetByIdAsync(contractId));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
