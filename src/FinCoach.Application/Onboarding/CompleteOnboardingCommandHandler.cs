using FinCoach.Application.Interfaces;
using FinCoach.Domain.Entities;
using FinCoach.Domain.Enums;
using MediatR;

namespace FinCoach.Application.Onboarding;

public class CompleteOnboardingCommandHandler : IRequestHandler<CompleteOnboardingCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CompleteOnboardingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CompleteOnboardingCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.IsOnboarded)
        {
            return; // Already onboarded
        }

        // 1. Update User Profile
        user.MonthlyIncome = request.MonthlyIncome;
        user.PrimaryCurrency = request.PrimaryCurrency;
        user.RiskAppetite = request.RiskAppetite;
        user.IsOnboarded = true;

        // 2. Create Initial Expenses (as Transactions)
        foreach (var expense in request.Expenses)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Type = TransactionType.Expense,
                Amount = expense.Amount,
                Currency = user.PrimaryCurrency,
                AmountInBaseCurrency = expense.Amount, // Simplified for onboarding
                Category = expense.Category,
                Date = DateTime.UtcNow,
                Notes = "Onboarding initial expense"
            };
            await _unitOfWork.Transactions.AddAsync(transaction);
        }

        // 3. Create Initial Goals
        foreach (var goalDto in request.Goals)
        {
            var goal = new Goal
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Name = goalDto.Name,
                TargetAmount = goalDto.TargetAmount,
                Currency = user.PrimaryCurrency,
                CurrentAmount = 0,
                TargetDate = goalDto.TargetDate,
                Status = GoalStatus.OnTrack,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.Goals.AddAsync(goal);
        }

        // 4. Create EMIs
        foreach (var emiDto in request.EMIs)
        {
            var emi = new EMI
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Type = emiDto.Type,
                Amount = emiDto.Amount,
                OriginalPrincipal = emiDto.OriginalPrincipal,
                InterestRate = emiDto.InterestRate,
                TotalTenure = emiDto.TotalTenure,
                PaidEmis = emiDto.PaidEmis,
                AccountNumber = emiDto.AccountNumber
            };
            await _unitOfWork.EMIs.AddAsync(emi);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
