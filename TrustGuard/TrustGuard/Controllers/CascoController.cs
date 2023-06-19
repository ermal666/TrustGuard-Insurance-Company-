using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustGuard.Application.Dtos;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;
using TrustGuard.Persistence.Repositories;

namespace TrustGuard.Application.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CascoController : Controller
{
    private readonly IOfferRepository _offerRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly ICascoRepository _cascoRepository;
    
    public CascoController(ICascoRepository cascoRepository, IMapper mapper, UserManager<User> userManager, IOfferRepository offerRepository)
    {
        _cascoRepository = cascoRepository;
        _mapper = mapper;
        _userManager = userManager;
        _offerRepository = offerRepository;
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
    public async Task<ActionResult<List<CascoInsurance>>> AddCasco(CreateCascoDto createCascoDto)
    {
        var casco = _mapper.Map<CascoInsurance>(createCascoDto);
        casco.User = _userManager.FindByIdAsync(createCascoDto.UserId).Result;
        casco.Offer = await _offerRepository.GetOfferById(x => x.Id == createCascoDto.OfferId).FirstOrDefaultAsync();


        return Ok(await _cascoRepository.AddCasco(casco));
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