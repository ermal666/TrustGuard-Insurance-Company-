using System.Linq.Expressions;
using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories
{
    public interface IOfferRepository
    {
        void CreateOffer(Offer offer);
        Task CreateOfferAsync(Offer offer);
        void CreateRange(List<Offer> offers);
        Task CreateRangeAsync(List<Offer> offers);
        void Delete(Offer offer);
        void DeleteRange(List<Offer> offers);
        IQueryable<Offer> GetAllOffers();
        IQueryable<Offer> GetOfferById(Expression<Func<Offer, bool>> expression);
        Task<bool> SaveChangesAsync();
        void Update(Offer offer);
        void UpdateRange(List<Offer> offers);
    }
}