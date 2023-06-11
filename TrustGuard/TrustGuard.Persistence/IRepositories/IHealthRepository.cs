using System.Collections.Generic;
using System.Threading.Tasks;
using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories;

public interface IHealthRepository
{
    Task<List<HealthInsurance>> GetAll();
    Offer GetOfferById(int offerId);
    void Save(HealthInsurance healthInsurance);

    List<HealthInsurance>? DeleteHealthCoverage(int id);
}