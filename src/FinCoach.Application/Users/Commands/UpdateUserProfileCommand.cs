using FinCoach.Application.Interfaces;
using FinCoach.Domain.Enums;
using MediatR;

namespace FinCoach.Application.Users.Commands;

public record UpdateUserProfileCommand(
    Guid UserId,
    decimal MonthlyIncome,
    string PrimaryCurrency,
    int RiskAppetite
) : IRequest;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserProfileCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        string oldCurrency = user.PrimaryCurrency;
        string newCurrency = request.PrimaryCurrency;

        // 1. Handle Currency Conversion if changed
        if (oldCurrency != newCurrency)
        {
            decimal rate = GetConversionRate(oldCurrency, newCurrency);
            
            // Convert Transactions
            var transactions = await _unitOfWork.Transactions.GetByUserIdAsync(user.Id);
            foreach (var tx in transactions)
            {
                tx.Amount *= rate;
                tx.Currency = newCurrency;
                await _unitOfWork.Transactions.UpdateAsync(tx);
            }

            // Convert Goals
            var goals = await _unitOfWork.Goals.GetByUserIdAsync(user.Id);
            foreach (var goal in goals)
            {
                goal.TargetAmount *= rate;
                goal.CurrentAmount *= rate;
                goal.Currency = newCurrency;
                await _unitOfWork.Goals.UpdateAsync(goal);
            }
        }

        // 2. Update User Profile
        user.MonthlyIncome = request.MonthlyIncome;
        user.PrimaryCurrency = newCurrency;
        user.RiskAppetite = (RiskAppetite)request.RiskAppetite;

        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private decimal GetConversionRate(string from, string to)
    {
        if (from == to) return 1.0m;
        
        // Hardcoded rates for expert simulation (AED, INR, USD)
        var rates = new Dictionary<(string, string), decimal>
        {
            { ("AED", "INR"), 22.5m },
            { ("INR", "AED"), 1 / 22.5m },
            { ("AED", "USD"), 0.27m },
            { ("USD", "AED"), 3.67m },
            { ("INR", "USD"), 0.012m },
            { ("USD", "INR"), 83.3m }
        };

        return rates.TryGetValue((from, to), out decimal rate) ? rate : 1.0m;
    }
}
