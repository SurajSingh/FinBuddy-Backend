using System.ComponentModel.DataAnnotations.Schema;
using FinCoach.Domain.Enums;

namespace FinCoach.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string CountryOfResidence { get; set; } = string.Empty;
    public string CountryOfOrigin { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal MonthlyIncome { get; set; }
    public string PrimaryCurrency { get; set; } = "AED";
    public string PhoneNumber { get; set; } = string.Empty;
    public string? CurrentOtp { get; set; }
    public RiskAppetite RiskAppetite { get; set; } = RiskAppetite.Moderate;
    public SubscriptionTier Tier { get; set; } = SubscriptionTier.Free;
    public bool IsOnboarded { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public virtual ICollection<EMI> EMIs { get; set; } = new List<EMI>();
}
