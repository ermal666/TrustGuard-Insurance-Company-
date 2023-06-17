using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using TrustGuard.Domain;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Persistence.Repositories;

public class AccidentRepository : IAccidentRepository
{
    private readonly AppDbContext _context;

    public AccidentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<AccidentInsurance>> GetAll()
    {
        var accidentsInsurances = await EntityFrameworkQueryableExtensions.ToListAsync(_context.AccidentInsurances);
        return accidentsInsurances;
    }

    public async Task<AccidentInsurance> GetSingleAccident(int id)
    {
        var accidentInsurance = await _context.AccidentInsurances.FindAsync(id);
        if (accidentInsurance is null)
            return null;
        return accidentInsurance;
    }

    public async Task<List<AccidentInsurance>> AddAccident(AccidentInsurance accidentInsurance)
    {
        _context.AccidentInsurances.Add(accidentInsurance);
        await _context.SaveChangesAsync();

        var accidentInsurancses = await EntityFrameworkQueryableExtensions.ToListAsync(_context.AccidentInsurances);
        return accidentInsurancses;
    }

    public async Task<List<AccidentInsurance>> UpdateAccident(int id, AccidentInsurance accidentInsurance)
    {
        var existingInsurance  = await _context.AccidentInsurances.FindAsync(id);
        if (existingInsurance is null)
            return null;

        existingInsurance.Proffesion = accidentInsurance.Proffesion;
        existingInsurance.CoverageAmount = accidentInsurance.CoverageAmount;
        existingInsurance.HealthConditions = accidentInsurance.HealthConditions;
        existingInsurance.Medications = accidentInsurance.Medications;
        existingInsurance.StartDate = accidentInsurance.StartDate;
        existingInsurance.EndDate = accidentInsurance.EndDate;
        existingInsurance.Offer = accidentInsurance.Offer;

        await _context.SaveChangesAsync();
        var updatedList = await _context.AccidentInsurances.ToListAsync();
        return updatedList;

    }

    public async Task<List<AccidentInsurance>> DeleteAccident(int id)
    {
        var existingAccident = await _context.AccidentInsurances.FindAsync(id);
        if (existingAccident is null)
            return null;
        
        _context.Remove(existingAccident);
        await _context.SaveChangesAsync();
        var updatedList = await _context.AccidentInsurances.ToListAsync();
        return updatedList;
        
    }
}