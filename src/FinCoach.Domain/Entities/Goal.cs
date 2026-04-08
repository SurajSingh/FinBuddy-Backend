using System.ComponentModel.DataAnnotations.Schema;
using FinCoach.Domain.Enums;

namespace FinCoach.Domain.Entities;

public class Goal
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal TargetAmount { get; set; }
    public string Currency { get; set; } = "AED";
    [Column(TypeName = "decimal(18, 2)")]
    public decimal CurrentAmount { get; set; }
    public DateTime TargetDate { get; set; }
    public GoalStatus Status { get; set; } = GoalStatus.OnTrack;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User User { get; set; } = null!;
}
