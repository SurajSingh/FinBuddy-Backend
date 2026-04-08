using FinCoach.Application.Interfaces;
using FinCoach.Domain.Entities;
using FinCoach.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinCoach.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id) => await _context.Users.FindAsync(id);

    public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber) => await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

    public async Task AddAsync(User user) => await _context.Users.AddAsync(user);

    public async Task UpdateAsync(User user) => _context.Users.Update(user);

    public async Task DeleteAsync(User user) => _context.Users.Remove(user);
}

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id) => await _context.Transactions.FindAsync(id);

    public async Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId) => await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();

    public async Task AddAsync(Transaction transaction) => await _context.Transactions.AddAsync(transaction);

    public async Task UpdateAsync(Transaction transaction) => _context.Transactions.Update(transaction);

    public async Task DeleteAsync(Transaction transaction) => _context.Transactions.Remove(transaction);
}

public class GoalRepository : IGoalRepository
{
    private readonly AppDbContext _context;

    public GoalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Goal?> GetByIdAsync(Guid id) => await _context.Goals.FindAsync(id);

    public async Task<IEnumerable<Goal>> GetByUserIdAsync(Guid userId) => await _context.Goals.Where(g => g.UserId == userId).ToListAsync();

    public async Task AddAsync(Goal goal) => await _context.Goals.AddAsync(goal);

    public async Task UpdateAsync(Goal goal) => _context.Goals.Update(goal);

    public async Task DeleteAsync(Goal goal) => _context.Goals.Remove(goal);
}

public class EMIRepository : IEMIRepository
{
    private readonly AppDbContext _context;

    public EMIRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EMI?> GetByIdAsync(Guid id) => await _context.EMIs.FindAsync(id);

    public async Task<IEnumerable<EMI>> GetByUserIdAsync(Guid userId) => await _context.EMIs.Where(e => e.UserId == userId).ToListAsync();

    public async Task AddAsync(EMI emi) => await _context.EMIs.AddAsync(emi);

    public async Task UpdateAsync(EMI emi) => _context.EMIs.Update(emi);

    public async Task DeleteAsync(EMI emi) => _context.EMIs.Remove(emi);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IUserRepository? _users;
    private ITransactionRepository? _transactions;
    private IGoalRepository? _goals;
    private IEMIRepository? _emis;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);
    public ITransactionRepository Transactions => _transactions ??= new TransactionRepository(_context);
    public IGoalRepository Goals => _goals ??= new GoalRepository(_context);
    public IEMIRepository EMIs => _emis ??= new EMIRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
