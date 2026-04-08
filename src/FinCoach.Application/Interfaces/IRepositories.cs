using FinCoach.Domain.Entities;

namespace FinCoach.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
    Task AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task DeleteAsync(Transaction transaction);
}

public interface IGoalRepository
{
    Task<Goal?> GetByIdAsync(Guid id);
    Task<IEnumerable<Goal>> GetByUserIdAsync(Guid userId);
    Task AddAsync(Goal goal);
    Task UpdateAsync(Goal goal);
    Task DeleteAsync(Goal goal);
}

public interface IEMIRepository
{
    Task<EMI?> GetByIdAsync(Guid id);
    Task<IEnumerable<EMI>> GetByUserIdAsync(Guid userId);
    Task AddAsync(EMI emi);
    Task UpdateAsync(EMI emi);
    Task DeleteAsync(EMI emi);
}

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ITransactionRepository Transactions { get; }
    IGoalRepository Goals { get; }
    IEMIRepository EMIs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
