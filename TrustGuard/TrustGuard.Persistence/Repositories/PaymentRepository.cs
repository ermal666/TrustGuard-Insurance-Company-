using System.Linq.Expressions;
using TrustGuard.Domain;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;

namespace TrustGuard.Persistence.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _dbContext;
    
            public PaymentRepository(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }
    
            public async Task<bool> SaveChangesAsync()
            {
                var complete = await _dbContext.SaveChangesAsync() > 0;
                return complete;
            }
    
            public IQueryable<Payment> GetAllPayments()
            {
                return _dbContext.Set<Payment>();
            }
    
            public IQueryable<Payment> GetPaymentById(Expression<Func<Payment, bool>> expression)
            {
    
                return _dbContext.Set<Payment>().Where(expression);
    
            }
    
            public void CreatePayment(Payment payment)
            {
                _dbContext.Set<Payment>().Add(payment);
    
            }
    
            public async Task CreatePaymentAsync(Payment payment)
            {
                await _dbContext.Set<Payment>().AddAsync(payment);
    
            }
    
    
            public void CreateRange(List<Payment> payments)
            {
                _dbContext.Set<Payment>().AddRange(payments);
            }
    
            public async Task CreateRangeAsync(List<Payment> payments)
            {
                await _dbContext.Set<Payment>().AddRangeAsync(payments);
            }
    
            public void Delete(Payment payment)
            {
                _dbContext.Set<Payment>().Remove(payment);
            }
            public void DeleteRange(List<Payment> payments)
            {
                _dbContext.Set<Payment>().RemoveRange(payments);
            }
    
            public void Update(Payment payment)
            {
                _dbContext.Set<Payment>().Update(payment);
            }
            public void UpdateRange(List<Payment> payments)
            {
                _dbContext.Set<Payment>().UpdateRange(payments);
            }
}