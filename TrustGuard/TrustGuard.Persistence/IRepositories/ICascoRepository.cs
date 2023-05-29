using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories;

public interface ICascoRepository
{
    Task<List<Casco>> GetAll();
    Task<Casco> GetSingleCasco(int id);
    Task<List<Casco>> AddCasco(Casco casco);
    Task<List<Casco>> UpdateCasco(int id, Casco casco);
    Task<Casco> DeleteCasco(int id);
}