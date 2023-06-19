using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Service.Services
{
    public class OfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<List<Offer>> GetAllOffers(int pageIndex = 1, int pageSize = 10)
        {
            var offers = await _offerRepository.GetAllOffers().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return offers ?? new List<Offer>();
        }

        public async Task<Offer> GetOfferById(int id)
        {
            var offer = await _offerRepository.GetOfferById(o => o.Id == id).FirstOrDefaultAsync();
            return offer ?? new Offer();
        }
    }
}
