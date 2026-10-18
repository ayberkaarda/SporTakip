using Microsoft.Extensions.DependencyInjection;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Services;

namespace SporTakip.Application;

public static class ApplicationServiceCollectionExtensions
{
    /// <summary>Uygulama servislerini kaydeder; repository arayüzleri ayrıca kaydedilmelidir.</summary>
    public static IServiceCollection AddSporTakipApplication(this IServiceCollection services)
    {
        services.AddScoped<ISporcuService, SporcuService>();
        services.AddScoped<IAntrenmanService, AntrenmanService>();
        services.AddScoped<ITakipService, TakipService>();
        services.AddScoped<IRaporService, RaporService>();
        return services;
    }
}
