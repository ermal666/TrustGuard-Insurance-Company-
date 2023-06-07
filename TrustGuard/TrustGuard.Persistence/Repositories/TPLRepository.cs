using Microsoft.EntityFrameworkCore;
using TrustGuard.Domain;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Persistence.Repositories;

public class TPLRepository : ITPLRepository
{
    private readonly AppDbContext _context;

    public TPLRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<TPLInsurance>> GetAllAsync()
    {
        return await _context.TPLInsurances.ToListAsync();
    }

    public async Task<TPLInsurance> GetSingleAsync(int id)
    {
        var tplInsurance = await _context.TPLInsurances.FindAsync(id);
        if (tplInsurance is null)
            return null;
        
        return tplInsurance;
    }

    public async Task<TPLInsurance> AddTplAsync(TPLInsurance tplInsurance)
    {
        _context.TPLInsurances.Add(tplInsurance);
        await _context.SaveChangesAsync();
        return tplInsurance;
    }

    public async Task<List<TPLInsurance>> UpdateTplAsync(int id, TPLInsurance tplInsurance)
    {
        var existingTPL = await _context.TPLInsurances.FindAsync(id);
        if (existingTPL is null)
            return null;

        existingTPL.PolicyNumber = tplInsurance.PolicyNumber;
        existingTPL.PolicyValidation = tplInsurance.PolicyValidation;
        existingTPL.VinNumber = tplInsurance.VinNumber;
        existingTPL.Plate = tplInsurance.Plate;
        existingTPL.Producer = tplInsurance.Producer;
        existingTPL.PurchaseDate = tplInsurance.PurchaseDate;

        await _context.SaveChangesAsync();
        return await _context.TPLInsurances.ToListAsync();

    }

    public async Task<TPLInsurance> DeleteTplAsync(int id)
    {
        var tplInsurance = await _context.TPLInsurances.FindAsync(id);
        if (tplInsurance is null)
            return null;

        _context.Remove(tplInsurance);
        await _context.SaveChangesAsync();
        return tplInsurance;
    }
}