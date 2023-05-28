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
    public class PropertyRepositroty : IPropertyRepositroty
    {
        private readonly AppDbContext _dbContext;

        public PropertyRepositroty(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var complete = await _dbContext.SaveChangesAsync() > 0;
            return complete;
        }

        public IQueryable<Property> GetAllProperties()
        {
            return _dbContext.Set<Property>();
        }

        public IQueryable<Property> GetPropertyById(Expression<Func<Property, bool>> expression)
        {

            return _dbContext.Set<Property>().Where(expression);

        }

        public void CreateProperty(Property property)
        {
            _dbContext.Set<Property>().Add(property);

        }

        public async Task CreatePropertyAsync(Property property)
        {
            await _dbContext.Set<Property>().AddAsync(property);

        }


        public void CreateRange(List<Property> properties)
        {
            _dbContext.Set<Property>().AddRange(properties);
        }

        public async Task CreateRangeAsync(List<Property> properties)
        {
            await _dbContext.Set<Property>().AddRangeAsync(properties);
        }

        public void Delete(Property property)
        {
            _dbContext.Set<Property>().Remove(property);
        }
        public void DeleteRange(List<Property> properties)
        {
            _dbContext.Set<Property>().RemoveRange(properties);
        }

        public void Update(Property property)
        {
            _dbContext.Set<Property>().Update(property);
        }
        public void UpdateRange(List<Property> properties)
        {
            _dbContext.Set<Property>().UpdateRange(properties);
        }
    }
}
