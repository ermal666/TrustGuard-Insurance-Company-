using System.Linq.Expressions;
using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories
{
    public interface ICarRepository
    {
        void CreateCar(Car car);
        Task CreateCarAsync(Car car);
        void CreateRange(List<Car> cars);
        Task CreateRangeAsync(List<Car> cars);
        void Delete(Car car);
        void DeleteRange(List<Car> cars);
        IQueryable<Car> GetAllCars();
        IQueryable<Car> GetCarById(Expression<Func<Car, bool>> expression);
        Task<bool> SaveChangesAsync();
        void Update(Car car);
        void UpdateRange(List<Car> cars);
    }
}