using System;

namespace FinCoach.Domain.Entities;

public class EMI
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Type { get; set; } = string.Empty;
    [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "decimal(18, 2)")]
    public decimal OriginalPrincipal { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "decimal(18, 2)")]
    public decimal InterestRate { get; set; }
    public int TotalTenure { get; set; }
    public int PaidEmis { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual User? User { get; set; }
}
