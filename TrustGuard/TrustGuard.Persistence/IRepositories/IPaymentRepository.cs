using System.Linq.Expressions;
using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories
{
    public interface IPaymentRepository
    {
        void CreatePayment(Payment payment);
        Task CreatePaymentAsync(Payment payment);
        void CreateRange(List<Payment> payments);
        Task CreateRangeAsync(List<Payment> payments);
        void Delete(Payment payment);
        void DeleteRange(List<Payment> payments);
        IQueryable<Payment> GetAllPayments();
        IQueryable<Payment> GetPaymentById(Expression<Func<Payment, bool>> expression);
        Task<bool> SaveChangesAsync();
        void Update(Payment payment);
        void UpdateRange(List<Payment> payments);
    }
}