using Microsoft.AspNetCore.Mvc;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Application.Controllers;

[Route("api/[controller]/[action]/")]
[ApiController]
public class CascoController : Controller
{
    private readonly ICascoRepository _cascoRepository;
    
    public CascoController(ICascoRepository cascoRepository)
    {
        _cascoRepository = cascoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Casco>>> GetAll()
    {
        var result =  await _cascoRepository.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Casco>> GetSingleCasco(int id)
    {
        var result = await _cascoRepository.GetSingleCasco(id);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<List<Casco>>> AddCasco(Casco casco)
    {
        var result = await _cascoRepository.AddCasco(casco);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult<List<Casco>>> UpdateCasco(int id, Casco casco)
    {
        var result = await _cascoRepository.UpdateCasco(id, casco);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Casco>> DeleteCasco(int id)
    {
        var result = await _cascoRepository.DeleteCasco(id);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }
}