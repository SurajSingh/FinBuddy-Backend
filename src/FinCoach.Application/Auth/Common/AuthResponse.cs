namespace FinCoach.Application.Auth.Common;

public record AuthResponse(
    Guid Id,
    string Email,
    string DisplayName,
    string Token,
    bool IsOnboarded,
    decimal MonthlyIncome,
    string PrimaryCurrency
);
