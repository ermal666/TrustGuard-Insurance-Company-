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
    
    public async Task<List<Casco>> GetAll()
    {
        var insurances = await _context.Cascos.ToListAsync();
        return insurances;
    }

    public async Task<Casco> GetSingleCasco(int id)
    {
        var cascoInsurance = await _context.Cascos.FindAsync(id);
        if (cascoInsurance is null)
            return null;

        return cascoInsurance;
    }

    public async Task<List<Casco>> AddCasco(Casco casco)
    {
        _context.Cascos.Add(casco);
        await _context.SaveChangesAsync();

        var cascoInsurances = await _context.Cascos.ToListAsync();
        return cascoInsurances;
    }

    public async Task<List<Casco>> UpdateCasco(int id, Casco casco)
    {
        var existingCasco = await _context.Cascos.FindAsync(id);
        if (existingCasco is null)
            return null;

        existingCasco.VinNumber = casco.VinNumber;
        existingCasco.CarModel = casco.CarModel;
        existingCasco.Plate = casco.Plate;
        existingCasco.Producer = casco.Producer;
        existingCasco.Color = casco.Color;
        existingCasco.EngineCapacity = casco.EngineCapacity;
        existingCasco.SeatingCapacity = casco.SeatingCapacity;
        existingCasco.PurchaseDate = casco.PurchaseDate;
        existingCasco.Offer = casco.Offer;

        await _context.SaveChangesAsync();
        var updatedCascoList = await _context.Cascos.ToListAsync();
        return updatedCascoList;
    }

    public async Task<Casco> DeleteCasco(int id)
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