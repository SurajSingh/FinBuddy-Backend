using FinCoach.Domain.Enums;

namespace FinCoach.Application.Onboarding;

public record InitialExpenseDto(string Category, decimal Amount);

public record InitialEMIDto(
    string Type, 
    decimal Amount, 
    decimal OriginalPrincipal, 
    decimal InterestRate, 
    int TotalTenure, 
    int PaidEmis, 
    string AccountNumber
);

public record InitialGoalDto(string Name, decimal TargetAmount, DateTime TargetDate);

public record CompleteOnboardingCommand(
    Guid UserId,
    decimal MonthlyIncome,
    string PrimaryCurrency,
    RiskAppetite RiskAppetite,
    List<InitialExpenseDto> Expenses,
    List<InitialEMIDto> EMIs,
    List<InitialGoalDto> Goals) : MediatR.IRequest;
