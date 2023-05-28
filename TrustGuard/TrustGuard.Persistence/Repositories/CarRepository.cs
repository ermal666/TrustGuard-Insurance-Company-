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
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _dbContext;

        public CarRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var complete = await _dbContext.SaveChangesAsync() > 0;
            return complete;
        }

        public IQueryable<Car> GetAllCars()
        {
            return _dbContext.Set<Car>();
        }

        public IQueryable<Car> GetCarById(Expression<Func<Car, bool>> expression)
        {

            return _dbContext.Set<Car>().Where(expression);

        }

        public void CreateCar(Car car)
        {
            _dbContext.Set<Car>().Add(car);

        }

        public async Task CreateCarAsync(Car car)
        {
            await _dbContext.Set<Car>().AddAsync(car);

        }


        public void CreateRange(List<Car> cars)
        {
            _dbContext.Set<Car>().AddRange(cars);
        }

        public async Task CreateRangeAsync(List<Car> cars)
        {
            await _dbContext.Set<Car>().AddRangeAsync(cars);
        }

        public void Delete(Car car)
        {
            _dbContext.Set<Car>().Remove(car);
        }
        public void DeleteRange(List<Car> cars)
        {
            _dbContext.Set<Car>().RemoveRange(cars);
        }

        public void Update(Car car)
        {
            _dbContext.Set<Car>().Update(car);
        }
        public void UpdateRange(List<Car> cars)
        {
            _dbContext.Set<Car>().UpdateRange(cars);
        }
    }
}
