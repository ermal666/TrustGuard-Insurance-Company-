using Microsoft.AspNetCore.Mvc;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Application.Controllers;

public class TPLController : ControllerBase
{
    private readonly ITPLRepository _tplRepository;

    public TPLController(ITPLRepository tplRepository)
    {
        _tplRepository = tplRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<TPLInsurance>>> GetAll()
    {
        return Ok(await _tplRepository.GetAllAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TPLInsurance>> GetSingle(int id)
    {
        var tplInsurance = await _tplRepository.GetSingleAsync(id);
        if (tplInsurance is null)
            return NotFound();

        return Ok(tplInsurance);
    }

    [HttpPost]
    public async Task<ActionResult<TPLInsurance>> AddTpl(TPLInsurance tplInsurance)
    {
        return Ok(await _tplRepository.AddTplAsync(tplInsurance));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<List<TPLInsurance>>> UpdateTpl(int id, TPLInsurance tplInsurance)
    {
        var insurance = await _tplRepository.UpdateTplAsync(id, tplInsurance);
        if (tplInsurance is null)
            return NotFound();

        return Ok(insurance);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<TPLInsurance>> DeleteTpl(int id)
    {
        var tplInsurance = _tplRepository.DeleteTplAsync(id);
        if (tplInsurance is null)
            return null;

        return Ok(tplInsurance);
    }

}