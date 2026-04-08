using Microsoft.Extensions.DependencyInjection;

namespace FinCoach.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        // Add FluentValidation later
        return services;
    }
}
