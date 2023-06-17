using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories;

public interface IAccidentRepository
{
    Task<List<AccidentInsurance>> GetAll();
    Task<AccidentInsurance> GetSingleAccident(int id);
    Task<List<AccidentInsurance>> AddAccident(AccidentInsurance accidentInsurance);
    Task<List<AccidentInsurance>> UpdateAccident(int id, AccidentInsurance accidentInsurance);
    Task<List<AccidentInsurance>> DeleteAccident(int id);
}