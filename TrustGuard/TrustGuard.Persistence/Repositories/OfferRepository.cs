using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Persistence.IRepositories;
using TrustGuard.Domain.Models;
using TrustGuard.Domain;

namespace TrustGuard.Persistence.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _dbContext;

        public OfferRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var complete = await _dbContext.SaveChangesAsync() > 0;
            return complete;
        }

        public IQueryable<Offer> GetAllOffers()
        {
            return _dbContext.Set<Offer>();
        }

        public IQueryable<Offer> GetOfferById(Expression<Func<Offer, bool>> expression)
        {

            return _dbContext.Set<Offer>().Where(expression);

        }

        public void CreateOffer(Offer offer)
        {
            _dbContext.Set<Offer>().Add(offer);

        }

        public async Task CreateOfferAsync(Offer offer)
        {
            await _dbContext.Set<Offer>().AddAsync(offer);

        }


        public void CreateRange(List<Offer> offers)
        {
            _dbContext.Set<Offer>().AddRange(offers);
        }

        public async Task CreateRangeAsync(List<Offer> offers)
        {
            await _dbContext.Set<Offer>().AddRangeAsync(offers);
        }

        public void Delete(Offer offer)
        {
            _dbContext.Set<Offer>().Remove(offer);
        }
        public void DeleteRange(List<Offer> offers)
        {
            _dbContext.Set<Offer>().RemoveRange(offers);
        }

        public void Update(Offer offer)
        {
            _dbContext.Set<Offer>().Update(offer);
        }
        public void UpdateRange(List<Offer> offers)
        {
            _dbContext.Set<Offer>().UpdateRange(offers);
        }
    }

}
