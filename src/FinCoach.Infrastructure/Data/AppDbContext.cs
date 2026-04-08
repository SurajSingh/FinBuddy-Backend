using FinCoach.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinCoach.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Goal> Goals { get; set; } = null!;
    public DbSet<EMI> EMIs { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Currency> Currencies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired(false).HasMaxLength(255);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
            entity.Property(e => e.MonthlyIncome).HasPrecision(18, 2);
        });

        // Transaction Configuration
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Transactions)
                  .HasForeignKey(e => e.UserId);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.AmountInBaseCurrency).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Goals)
                  .HasForeignKey(e => e.UserId);
            entity.Property(e => e.TargetAmount).HasPrecision(18, 2);
            entity.Property(e => e.CurrentAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<EMI>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                  .WithMany(u => u.EMIs)
                  .HasForeignKey(e => e.UserId);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.OriginalPrincipal).HasPrecision(18, 2);
            entity.Property(e => e.InterestRate).HasPrecision(18, 2);
        });

        // Country Configuration
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(10);
            entity.HasIndex(e => e.Code).IsUnique();
        });

        // Currency Configuration
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(10);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // Seed Data
        modelBuilder.Entity<Country>().HasData(
            new Country { Id = new Guid("A2CFA51A-64E0-40B0-ADCB-3BB6D8543719"), Name = "United Arab Emirates", Code = "AE", PhoneCode = "+971", Flag = "🇦🇪", IsEnabled = true },
            new Country { Id = new Guid("17453330-7AD0-4BD1-9AAE-8C986468F92A"), Name = "India", Code = "IN", PhoneCode = "+91", Flag = "🇮🇳", IsEnabled = true },
            new Country { Id = new Guid("16FBAEB5-4EB9-4645-8199-343453A6BB65"), Name = "United States", Code = "US", PhoneCode = "+1", Flag = "🇺🇸", IsEnabled = true }
        );

        modelBuilder.Entity<Currency>().HasData(
            new Currency { Id = Guid.NewGuid(), Code = "AED", Name = "UAE Dirham", Symbol = "د.إ", IsEnabled = true },
            new Currency { Id = Guid.NewGuid(), Code = "INR", Name = "Indian Rupee", Symbol = "₹", IsEnabled = true },
            new Currency { Id = Guid.NewGuid(), Code = "USD", Name = "US Dollar", Symbol = "$", IsEnabled = true }
        );

        // Global decimal precision enforcement for all entities
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }
    }
}
