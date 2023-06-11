using System.Collections.Generic;
using System.Threading.Tasks;
using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories;

public interface ITPLRepository
{
    Task<List<TPLInsurance>> GetAllAsync();
    Task<TPLInsurance> GetSingleAsync(int id);
    Task<TPLInsurance> AddTplAsync(TPLInsurance tplInsurance);
    Task<List<TPLInsurance>> UpdateTplAsync(int id, TPLInsurance tplInsurance);
    Task<TPLInsurance> DeleteTplAsync(int id);
}