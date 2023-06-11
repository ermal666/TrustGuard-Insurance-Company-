using System.Collections.Generic;
using System.Threading.Tasks;
using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories;

public interface ICascoRepository
{
    Task<List<CascoInsurance>> GetAll();
    Task<CascoInsurance> GetSingleCasco(int id);
    Task<List<CascoInsurance>> AddCasco(CascoInsurance cascoInsurance);
    Task<List<CascoInsurance>> UpdateCasco(int id, CascoInsurance cascoInsurance);
    Task<CascoInsurance> DeleteCasco(int id);
}