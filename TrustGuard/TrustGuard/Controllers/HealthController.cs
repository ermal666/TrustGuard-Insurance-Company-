using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using TrustGuard.Domain;
using TrustGuard.Domain.Enums;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IHealthRepository _healthRepository;

    public HealthController(IHealthRepository healthRepository)
    {
        _healthRepository = healthRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<HealthInsurance>>> GetAll()
    {
        return Ok(await _healthRepository.GetAll());
    }

    [HttpPost]
    public IActionResult CalculatePrice(HealthInsurance healthInsurance)
    {
        Offer selectedOffer = _healthRepository.GetOfferById(healthInsurance.OfferId);
        
        if (selectedOffer == null)
        {
            return BadRequest("Invalid offer selected.");
        }

        // Calculate the price based on the selected payment option and offer
        if (healthInsurance.PaymentOption == PaymentOptions.Monthly)
        {
            healthInsurance.Price = selectedOffer.Price;
        }
        else if (healthInsurance.PaymentOption == PaymentOptions.Yearly)
        {
            healthInsurance.Price = selectedOffer.Price * 12;
        }
        else
        {
            return BadRequest("Invalid payment option selected.");
        }

        // Save the healthInsurance object with the calculated price to the database
        _healthRepository.Save(healthInsurance);

        return Ok(healthInsurance.Price);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<HealthInsurance>>> DeleteHealthCoverage(int id)
    {
        var result =  _healthRepository.DeleteHealthCoverage(id);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }

}