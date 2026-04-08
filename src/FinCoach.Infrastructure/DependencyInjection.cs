using FinCoach.Application.Interfaces;
using FinCoach.Infrastructure.Data;
using FinCoach.Infrastructure.Repositories;
using FinCoach.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinCoach.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Token Service
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
