namespace FinCoach.Domain.Entities;

public class Country
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // e.g., AE, IN, US
    public string PhoneCode { get; set; } = string.Empty; // e.g., +971, +91, +1
    public string? Flag { get; set; } // Emoji or URL
    public bool IsEnabled { get; set; } = true;
}
