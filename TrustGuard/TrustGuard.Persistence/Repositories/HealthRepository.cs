using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrustGuard.Domain;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Persistence.Repositories;

public class HealthRepository : IHealthRepository
{
    private readonly AppDbContext _context;

    public HealthRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<HealthInsurance>> GetAll()
    {
        var healthInsurances = await _context.HealthInsurances.ToListAsync();
        return healthInsurances;
    }
    public Offer GetOfferById(int offerId)
    {
        return _context.Offers.FirstOrDefault(o => o.OfferId == offerId);
    }

    public void Save(HealthInsurance healthInsurance)
    {
        _context.HealthInsurances.Add(healthInsurance);
        _context.SaveChanges();
    }

    public List<HealthInsurance>? DeleteHealthCoverage(int id)
    {
        var result = _context.HealthInsurances.Find(id);
        if (result is null)
            return null;
        
        _context.Remove(result);
        return _context.HealthInsurances.ToList();
    }
}