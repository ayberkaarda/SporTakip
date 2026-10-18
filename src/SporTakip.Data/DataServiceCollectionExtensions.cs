using Microsoft.Extensions.DependencyInjection;
using SporTakip.Application.Abstractions.Repositories;

namespace SporTakip.Data;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddSporTakipData(this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddSingleton(new SchemaMigrator(connectionString));
        services.AddSingleton<IDbConnectionFactory>(new SqlConnectionFactory(connectionString));
        services.AddScoped<ISporcuRepository, SporcuRepository>();
        services.AddScoped<IAntrenmanRepository, AntrenmanRepository>();
        services.AddScoped<ITakipRepository, TakipRepository>();
        services.AddScoped<IOlcumRepository, OlcumRepository>();
        return services;
    }
}
