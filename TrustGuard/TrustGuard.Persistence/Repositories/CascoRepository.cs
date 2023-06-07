using Microsoft.EntityFrameworkCore;
using TrustGuard.Domain;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Persistence.Repositories;

public class CascoRepository : ICascoRepository
{
    private readonly AppDbContext _context;

    public CascoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CascoInsurance>> GetAll()
    {
        var insurances = await _context.Cascos.ToListAsync();
        return insurances;
    }

    public async Task<CascoInsurance> GetSingleCasco(int id)
    {
        var cascoInsurance = await _context.Cascos.FindAsync(id);
        if (cascoInsurance is null)
            return null;

        return cascoInsurance;
    }

    public async Task<List<CascoInsurance>> AddCasco(CascoInsurance cascoInsurance)
    {
        _context.Cascos.Add(cascoInsurance);
        await _context.SaveChangesAsync();

        var cascoInsurances = await _context.Cascos.ToListAsync();
        return cascoInsurances;
    }

    public async Task<List<CascoInsurance>> UpdateCasco(int id, CascoInsurance cascoInsurance)
    {
        var existingCasco = await _context.Cascos.FindAsync(id);
        if (existingCasco is null)
            return null;

        existingCasco.VinNumber = cascoInsurance.VinNumber;
        existingCasco.CarModel = cascoInsurance.CarModel;
        existingCasco.Plate = cascoInsurance.Plate;
        existingCasco.Producer = cascoInsurance.Producer;
        existingCasco.Color = cascoInsurance.Color;
        existingCasco.EngineCapacity = cascoInsurance.EngineCapacity;
        existingCasco.SeatingCapacity = cascoInsurance.SeatingCapacity;
        existingCasco.PurchaseDate = cascoInsurance.PurchaseDate;
        existingCasco.Offer = cascoInsurance.Offer;

        await _context.SaveChangesAsync();
        var updatedCascoList = await _context.Cascos.ToListAsync();
        return updatedCascoList;
    }

    public async Task<CascoInsurance> DeleteCasco(int id)
    {
        var existingCasco = await _context.Cascos.FindAsync(id);
        if (existingCasco is null)
            return null;
        
        _context.Remove(existingCasco);
        await _context.SaveChangesAsync();
        return existingCasco;
        //changes to be done
    }
}