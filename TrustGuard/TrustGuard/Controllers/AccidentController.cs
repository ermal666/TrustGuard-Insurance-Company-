using Microsoft.AspNetCore.Mvc;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Application.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccidentController : Controller
{
    private readonly IAccidentRepository _accidentRepository;
    
    public AccidentController(IAccidentRepository accidentRepository)
    {
        _accidentRepository = accidentRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<AccidentInsurance>>> GetAll()
    {
        return Ok(await _accidentRepository.GetAll());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<AccidentInsurance>> GetSingleAccident(int id)
    {
        var result = await _accidentRepository.GetSingleAccident(id);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<List<AccidentInsurance>>> AddAccident(AccidentInsurance accidentInsurance)
    {
        return Ok(await _accidentRepository.AddAccident(accidentInsurance));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<List<AccidentInsurance>>> UpdateAccident(int id, AccidentInsurance accidentInsurance)
    {
        var result = await _accidentRepository.UpdateAccident(id, accidentInsurance);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AccidentInsurance>> DeleteAccident(int id)
    {
        var result = await _accidentRepository.DeleteAccident(id);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }
}