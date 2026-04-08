namespace FinCoach.Domain.Entities;

public class Currency
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty; // e.g., AED, INR, USD
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
}
