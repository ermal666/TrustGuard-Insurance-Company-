using Microsoft.AspNetCore.Mvc;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Application.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CascoController : ControllerBase
{
    private readonly ICascoRepository _cascoRepository;
    
    public CascoController(ICascoRepository cascoRepository)
    {
        _cascoRepository = cascoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CascoInsurance>>> GetAll()
    {
        return Ok(await _cascoRepository.GetAll());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CascoInsurance>> GetSingleCasco(int id)
    {
        var result = await _cascoRepository.GetSingleCasco(id);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<List<CascoInsurance>>> AddCasco(CascoInsurance cascoInsurance)
    {
        return Ok(await _cascoRepository.AddCasco(cascoInsurance));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<List<CascoInsurance>>> UpdateCasco(int id, CascoInsurance cascoInsurance)
    {
        var result = await _cascoRepository.UpdateCasco(id, cascoInsurance);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CascoInsurance>> DeleteCasco(int id)
    {
        var result = await _cascoRepository.DeleteCasco(id);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }
}