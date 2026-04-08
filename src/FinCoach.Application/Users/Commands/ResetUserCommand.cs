using FinCoach.Application.Interfaces;
using MediatR;

namespace FinCoach.Application.Users.Commands;

public record ResetUserCommand(Guid UserId) : IRequest;

public class ResetUserCommandHandler : IRequestHandler<ResetUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ResetUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ResetUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        // 1. Delete all transactions
        var transactions = await _unitOfWork.Transactions.GetByUserIdAsync(user.Id);
        foreach (var tx in transactions)
        {
            await _unitOfWork.Transactions.DeleteAsync(tx);
        }

        // 2. Delete all goals
        var goals = await _unitOfWork.Goals.GetByUserIdAsync(user.Id);
        foreach (var goal in goals)
        {
            await _unitOfWork.Goals.DeleteAsync(goal);
        }

        // 3. Reset onboarding status
        user.IsOnboarded = false;
        user.MonthlyIncome = 0;
        user.PrimaryCurrency = "AED";

        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
