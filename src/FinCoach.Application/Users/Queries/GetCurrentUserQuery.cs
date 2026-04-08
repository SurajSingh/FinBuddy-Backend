using FinCoach.Application.Interfaces;
using MediatR;

namespace FinCoach.Application.Users.Queries;

public record UserProfileDto(
    Guid Id,
    string Email,
    string DisplayName,
    decimal MonthlyIncome,
    string PrimaryCurrency,
    int RiskAppetite,
    bool IsOnboarded,
    List<InitialExpenseDto> Expenses,
    List<InitialEMIDto> EMIs
);

public record InitialExpenseDto(string Category, decimal Amount);

public record InitialEMIDto(
    Guid Id,
    string Type, 
    decimal Amount, 
    decimal OriginalPrincipal, 
    decimal InterestRate, 
    int TotalTenure, 
    int PaidEmis, 
    string AccountNumber
);

public record GetCurrentUserQuery(Guid UserId) : IRequest<UserProfileDto>;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserProfileDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCurrentUserQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserProfileDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return new UserProfileDto(
            user.Id,
            user.Email,
            user.DisplayName,
            user.MonthlyIncome,
            user.PrimaryCurrency,
            (int)user.RiskAppetite,
            user.IsOnboarded,
            user.Transactions.Where(t => t.Notes == "Onboarding initial expense").Select(t => new InitialExpenseDto(t.Category, t.Amount)).ToList(),
            user.EMIs.Select(e => new InitialEMIDto(
                e.Id,
                e.Type,
                e.Amount,
                e.OriginalPrincipal,
                e.InterestRate,
                e.TotalTenure,
                e.PaidEmis,
                e.AccountNumber
            )).ToList()
        );
    }
}
