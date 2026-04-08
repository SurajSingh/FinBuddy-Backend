using System.ComponentModel.DataAnnotations.Schema;
using FinCoach.Domain.Enums;

namespace FinCoach.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public TransactionType Type { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "AED";
    [Column(TypeName = "decimal(18, 2)")]
    public decimal AmountInBaseCurrency { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? ReceiptBlobUrl { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
}
