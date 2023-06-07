using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories
{
    public interface IInsuranceRepository
    {
        void CreateInsurance(Insurance insurance);
        Task CreateInsuranceAsync(Insurance insurance);
        void CreateRange(List<Insurance> insurances);
        Task CreateRangeAsync(List<Insurance> insurances);
        void Delete(Insurance insurance);
        void DeleteRange(List<Insurance> insurances);
        IQueryable<Insurance> GetAllInsurances();
        IQueryable<Insurance> GetInsuranceById(Expression<Func<Insurance, bool>> expression);
        Task<bool> SaveChangesAsync();
        void Update(Insurance insurance);
        void UpdateRange(List<Insurance> insurances);
    }
}