using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Domain;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Persistence.Repositories
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly AppDbContext _dbContext;

        public InsuranceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var complete = await _dbContext.SaveChangesAsync() > 0;
            return complete;
        }

        public IQueryable<Insurance> GetAllInsurances()
        {
            return _dbContext.Set<Insurance>();
        }

        public IQueryable<Insurance> GetInsuranceById(Expression<Func<Insurance, bool>> expression)
        {

            return _dbContext.Set<Insurance>().Where(expression);

        }

        public void CreateInsurance(Insurance insurance)
        {
            _dbContext.Set<Insurance>().Add(insurance);

        }

        public async Task CreateInsuranceAsync(Insurance insurance)
        {
            await _dbContext.Set<Insurance>().AddAsync(insurance);

        }


        public void CreateRange(List<Insurance> insurances)
        {
            _dbContext.Set<Insurance>().AddRange(insurances);
        }

        public async Task CreateRangeAsync(List<Insurance> insurances)
        {
            await _dbContext.Set<Insurance>().AddRangeAsync(insurances);
        }

        public void Delete(Insurance insurance)
        {
            _dbContext.Set<Insurance>().Remove(insurance);
        }
        public void DeleteRange(List<Insurance> insurances)
        {
            _dbContext.Set<Insurance>().RemoveRange(insurances);
        }

        public void Update(Insurance insurance)
        {
            _dbContext.Set<Insurance>().Update(insurance);
        }
        public void UpdateRange(List<Insurance> insurances)
        {
            _dbContext.Set<Insurance>().UpdateRange(insurances);
        }
    }
}
