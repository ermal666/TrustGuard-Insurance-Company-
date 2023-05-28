using System.Linq.Expressions;
using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories
{
    public interface IPropertyRepositroty
    {
        void CreateProperty(Property property);
        Task CreatePropertyAsync(Property property);
        void CreateRange(List<Property> properties);
        Task CreateRangeAsync(List<Property> properties);
        void Delete(Property property);
        void DeleteRange(List<Property> properties);
        IQueryable<Property> GetAllProperties();
        IQueryable<Property> GetPropertyById(Expression<Func<Property, bool>> expression);
        Task<bool> SaveChangesAsync();
        void Update(Property property);
        void UpdateRange(List<Property> properties);
    }
}